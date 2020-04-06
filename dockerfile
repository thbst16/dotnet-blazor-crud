# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src



COPY BlazorWasmDocker.csproj .
RUN dotnet restore "BlazorWasmDocker.csproj"
COPY . .
RUN dotnet build "BlazorWasmDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWasmDocker.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/BlazorWasmDocker/dist .
COPY nginx.conf /etc/nginx/nginx.conf