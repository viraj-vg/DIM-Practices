using FintechJobPortal.Services.Interfaces;
using FintechJobPortal.Services.Providers;
using FintechJobPortal.Services.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinPulse - Fintech Job Portal & Aggregator API",
        Version = "v1",
        Description = "Enterprise Job Aggregator API searching and consolidating fintech roles across HFT, Quant, Payments, Web3, Neo-Banking, WealthTech, and InsurTech."
    });
});

// Register Job Data Providers
builder.Services.AddSingleton<IJobProvider, HedgeFundQuantProvider>();
builder.Services.AddSingleton<IJobProvider, PaymentsFintechProvider>();
builder.Services.AddSingleton<IJobProvider, CryptoBlockchainProvider>();
builder.Services.AddSingleton<IJobProvider, NeoBankingProvider>();
builder.Services.AddSingleton<IJobProvider, WealthTechProvider>();
builder.Services.AddSingleton<IJobProvider, InsurTechRiskProvider>();

// Register Core Aggregator & Analytics Services
builder.Services.AddSingleton<IJobAggregatorService, JobAggregatorService>();
builder.Services.AddScoped<IMarketAnalyticsService, MarketAnalyticsService>();

// CORS configuration for local testing and multi-origin consumption
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || true) // Enable Swagger for interactive API exploration
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinPulse API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");

// Serve static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Fallback to index.html for SPA routing if needed
app.MapFallbackToFile("index.html");

app.Run();
