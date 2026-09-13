using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using FintechJobPortal.Services.Providers;
using FintechJobPortal.Services.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace FintechJobPortal.Tests;

public class AnalyticsServiceTests
{
    private readonly IMarketAnalyticsService _analyticsService;

    public AnalyticsServiceTests()
    {
        var providers = new List<IJobProvider>
        {
            new HedgeFundQuantProvider(),
            new PaymentsFintechProvider(),
            new CryptoBlockchainProvider(),
            new NeoBankingProvider(),
            new WealthTechProvider(),
            new InsurTechRiskProvider()
        };

        var aggregatorService = new JobAggregatorService(providers, NullLogger<JobAggregatorService>.Instance);
        _analyticsService = new MarketAnalyticsService(aggregatorService);
    }

    [Fact]
    public async Task GetMarketIntelligenceAsync_ShouldComputeValidFintechMetrics()
    {
        // Act
        var stats = await _analyticsService.GetMarketIntelligenceAsync();

        // Assert
        Assert.NotNull(stats);
        Assert.True(stats.TotalOpenings > 0, "Total openings should be positive");
        Assert.True(stats.AverageSalary > 50000, "Average salary should be realistic fintech compensation");
        Assert.True(stats.HighestSalary >= 300000, "Top tier fintech compensation should reach $300k+");
        Assert.True(stats.RemotePercentage >= 0 && stats.RemotePercentage <= 100);
        Assert.NotEmpty(stats.CategorySalaries);
        Assert.NotEmpty(stats.TopSkills);
        Assert.NotEmpty(stats.TopHiringHubs);
        Assert.NotEmpty(stats.TopHiringCompanies);
    }

    [Fact]
    public async Task GetMarketIntelligenceAsync_TopSkills_ShouldIncludeCommonFintechTech()
    {
        // Act
        var stats = await _analyticsService.GetMarketIntelligenceAsync();

        // Assert
        var skillNames = stats.TopSkills.Select(s => s.Skill).ToList();
        Assert.Contains("C#", skillNames);
    }
}
