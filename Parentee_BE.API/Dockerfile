FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Copy đúng từng project
COPY ["Parentee_BE.AI/Parentee_BE.AI.csproj", "Parentee_BE.AI/"]
COPY ["Parentee_BE.API/Parentee_BE.API.csproj", "Parentee_BE.API/"]
COPY ["Parentee_BE.BLL/Parentee_BE.BLL.csproj", "Parentee_BE.BLL/"]
COPY ["Parentee_BE.DAL/Parentee_BE.DAL.csproj", "Parentee_BE.DAL/"]

RUN dotnet restore "Parentee_BE.API/Parentee_BE.API.csproj"

COPY . .
WORKDIR "/src/Parentee_BE.API"
RUN dotnet build "./Parentee_BE.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Parentee_BE.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parentee_BE.API.dll"]
