### PREPARE
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201 AS build
WORKDIR /src

# Copy csproj and sln and restore as distinct layers
COPY BlazorCrud.AzureSetup/*.csproj BlazorCrud.AzureSetup/
COPY BlazorCrud.Client/*.csproj BlazorCrud.Client/
COPY BlazorCrud.Server/*.csproj BlazorCrud.Server/
COPY BlazorCrud.Shared/*.csproj BlazorCrud.Shared/
COPY BlazorCrud.Tests.API/*.csproj BlazorCrud.Tests.API/
COPY BlazorCrud.Tests.E2E/*.csproj BlazorCrud.Tests.E2E/
COPY BlazorCrud.sln .
RUN dotnet restore

### BUILD
COPY . .
RUN dotnet build "BlazorCrud.sln" -c Release -o /app

### PUBLISH
FROM build as publish
COPY . .
RUN dotnet publish "BlazorCrud.sln" -c Release -o /app

### RUNTIME IMAGE
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "BlazorCrud.Server.dll"]