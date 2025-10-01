using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Parentee_BE.API.OpenAPI;

public class OrderHttpMethodsFilter : IDocumentFilter
{
    // Desired order
    private static readonly string[] Order = { "get", "put", "post", "delete" };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths)
        {
            var orderedOps = path.Value.Operations
                .OrderBy(op => Array.IndexOf(Order, op.Key.ToString().ToLower()))
                .ToList();

            path.Value.Operations.Clear();
            foreach (var op in orderedOps)
            {
                path.Value.Operations.Add(op.Key, op.Value);
            }
        }
    }
}