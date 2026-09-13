# ==============================================================================
# Multi-Stage Dockerfile for FinPulse Fintech Job Portal & Aggregator
# Target Framework: .NET 8.0 LTS
# ==============================================================================

# ------------------------------------------------------------------------------
# Stage 1: Build & Restore
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy Solution and Project files for layer caching
COPY ["FintechJobPortal.sln", "./"]
COPY ["src/FintechJobPortal.Core/FintechJobPortal.Core.csproj", "src/FintechJobPortal.Core/"]
COPY ["src/FintechJobPortal.Services/FintechJobPortal.Services.csproj", "src/FintechJobPortal.Services/"]
COPY ["src/FintechJobPortal.Web/FintechJobPortal.Web.csproj", "src/FintechJobPortal.Web/"]
COPY ["tests/FintechJobPortal.Tests/FintechJobPortal.Tests.csproj", "tests/FintechJobPortal.Tests/"]

# Restore dependencies
RUN dotnet restore "FintechJobPortal.sln"

# Copy full source tree
COPY . .

# Build solution in Release configuration
RUN dotnet build "FintechJobPortal.sln" -c Release --no-restore

# ------------------------------------------------------------------------------
# Stage 2: Automated Unit & Integration Tests
# ------------------------------------------------------------------------------
FROM build AS test
WORKDIR /src/tests/FintechJobPortal.Tests
RUN dotnet test --no-build -c Release --logger "console;verbosity=detailed"

# ------------------------------------------------------------------------------
# Stage 3: Publish Application Binaries
# ------------------------------------------------------------------------------
FROM build AS publish
WORKDIR /src/src/FintechJobPortal.Web
RUN dotnet publish "FintechJobPortal.Web.csproj" \
    -c Release \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

# ------------------------------------------------------------------------------
# Stage 4: Production Lightweight Alpine Runtime
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Install curl for container health check
RUN apk add --no-cache curl icu-libs

# Set environment variables
ENV ASPNETCORE_HTTP_PORTS=8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Create non-root app user for security best practices
USER app

# Copy compiled assets from publish stage
COPY --from=publish --chown=app:app /app/publish .

# Expose standard port
EXPOSE 8080

# Health check probe
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "FintechJobPortal.Web.dll"]
