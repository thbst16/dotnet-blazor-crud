### PREPARE
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src

### Copy csproj and sln and restore as distinct layers
COPY Blazorcrud.Client/*.csproj Blazorcrud.Client/
COPY Blazorcrud.Server/*.csproj Blazorcrud.Server/
COPY Blazorcrud.Shared/*.csproj Blazorcrud.Shared/
COPY Blazorcrud.sln .
RUN dotnet restore

### PUBLISH
FROM build-env as publish-env
COPY . .
RUN dotnet publish "Blazorcrud.sln" -c Release -o /app

### RUNTIME IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime-env
WORKDIR /app
COPY --from=publish-env /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Blazorcrud.Server.dll"]