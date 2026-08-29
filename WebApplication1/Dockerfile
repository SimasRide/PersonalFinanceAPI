# Backend Dockerfile (multi-stage)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia apenas o csproj e restaura para usar cache
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
RUN dotnet restore "WebApplication1/WebApplication1.csproj"

# Copia todo o resto e publica
COPY . .
WORKDIR /src/WebApplication1
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5287
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
