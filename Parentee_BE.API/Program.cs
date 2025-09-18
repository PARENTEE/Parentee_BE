using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Embeddings;
using Parentee_BE.AI.Services;
using Parentee_BE.BLL.Helpers;
using Parentee_BE.BLL.Services.Implements;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Repositories;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.Middlewares;
using Qdrant.Client;
using PineconeClient = Pinecone.PineconeClient;

var builder = WebApplication.CreateBuilder(args);

#region Handle Environment Variables

DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

#endregion

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = true;
});

#region Configuration

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();
#endregion

#region Implement Swagger

builder.Services.AddEndpointsApiExplorer(); // Required for Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "PARENTEE Backend",
        Version = "v1",
        Description = "API for PARENTEE."
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \n\r Enter 'Bearer' [space] and then your token in the text input below.\n\r Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});


#endregion

#region Add database context

// Hash Password
foreach (var account in SeedingData.Users)
{
    account.Password = PasswordHelper.HashPassword(account.Password);
}

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("PostgresConnectionString"),
            npgsqlOptions => npgsqlOptions
                .EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null
                )
        )
        .UseSeeding((context, _) => SeedingData.Seed(context))
        .UseAsyncSeeding(
            async (context, _, cancellationToken) => await SeedingData.SeedAsync(context, cancellationToken)
        )
        .LogTo(Console.WriteLine, LogLevel.Information)
);

#endregion

#region Implement Authentication and Authorization

// Add Authentication and Authorization using JWT
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuers = builder.Configuration.GetSection("JWT:ValidIssuers").Get<string[]>(),
            ValidAudiences = builder.Configuration.GetSection("JWT:ValidAudiences").Get<string[]>(),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // for 401
                context.HandleResponse();

                throw new UnauthorizedException(
                    $"Authentication failed: {context.Error ?? "invalid_token"}");
            },
            OnForbidden = _ => throw new ForbiddenException("You do not have permission to access this resource.")
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
      options.ClientId = builder.Configuration["Google:ClientId"];
      options.ClientSecret = builder.Configuration["Google:ClientSecret"];
      options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    });

// Add Roles for Authorization
// builder.Services.AddAuthorizationBuilder()
//     .AddPolicy("RequireAdminRole", policy => policy.RequireRole(Role.Admin))
//     .AddPolicy("RequireStaffRole", policy => policy.RequireRole(Role.Staff))
//     .AddPolicy("RequireMemberRole", policy => policy.RequireRole(Role.Member));

#endregion

#region Implement CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

#endregion
    
# region Implement DI for Project Services

builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAiService, AiService>();

builder.Services.AddScoped<TokenHelper>();

builder.Services.AddScoped<IChildService, ChildService>();
#endregion

#region Other services

// Add AutoMapper
// Scan the whole assembly for profiles
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Add Semantic Kernel

#pragma warning disable SKEXP0010
var llmModel = "gemini-2.0-flash";
var llmApiKey = "AIzaSyCmtp4ctiu7RcF_Gij0bZILzDM5ZvbkoK4";
var embeddingModel = "gemini-embedding-001";
var embeddingApiKey = "AIzaSyCmtp4ctiu7RcF_Gij0bZILzDM5ZvbkoK4";
var vectoreStoreConnectionString = "";
var vectoreStoreApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2Nlc3MiOiJtIn0.JUla-V8J09Gi_3vThJBKnRTGKDhWOv9X5RCgRcGjN4U";

// LLM
builder.Services.AddGoogleAIGeminiChatCompletion(llmModel, llmApiKey);

// Embedding
builder.Services.AddScoped<ITextEmbeddingGenerationService>(sp =>
{
    return new GoogleAITextEmbeddingGenerationService(
        modelId: embeddingModel,
        apiKey: embeddingApiKey
    );
});

// Pinecone
// builder.Services.AddSingleton<PineconeClient>(
//     sp => new PineconeClient(vectoreStoreApiKey));
// builder.Services.AddPineconeVectorStore();

// Qdrant
builder.Services.AddSingleton<QdrantClient>(sp => 
    new QdrantClient(
        host: "620048c6-bd62-406b-b7c5-a52007362054.us-east4-0.gcp.cloud.qdrant.io", // cloud URL
        https: true,                                   // cloud uses HTTPS
        apiKey: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2Nlc3MiOiJtIn0.C-hVUMuh_KQ-TMi6zfqEjv8HTWZsRexXlRXgAnUs30I"                  // from Qdrant Cloud dashboard
    )
);

builder.Services.AddQdrantVectorStore();

// Semantic Kernel
builder.Services.AddScoped<Kernel>(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();

    kernelBuilder.AddGoogleAIGeminiChatCompletion(llmModel, llmApiKey);
    kernelBuilder.AddGoogleAIEmbeddingGenerator(embeddingModel, embeddingApiKey);
    
    return kernelBuilder.Build();
});

// RAGChatService
builder.Services.AddScoped<RAGChatService>();

#endregion

#region Configure API behavior

// Disable automatic model state validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Applies any pending migrations
}

app.Run();