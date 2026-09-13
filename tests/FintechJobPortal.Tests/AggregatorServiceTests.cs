using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using FintechJobPortal.Services.Providers;
using FintechJobPortal.Services.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace FintechJobPortal.Tests;

public class AggregatorServiceTests
{
    private readonly IJobAggregatorService _aggregatorService;

    public AggregatorServiceTests()
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

        _aggregatorService = new JobAggregatorService(providers, NullLogger<JobAggregatorService>.Instance);
    }

    [Fact]
    public async Task GetAllJobsAsync_ShouldReturnAggregatedJobsFromAllProviders()
    {
        // Act
        var jobs = await _aggregatorService.GetAllJobsAsync();

        // Assert
        Assert.NotNull(jobs);
        Assert.True(jobs.Count >= 10, "Expected at least 10 aggregated jobs across all fintech domains");
        Assert.Contains(jobs, j => j.Company.Name == "Citadel Securities");
        Assert.Contains(jobs, j => j.Company.Name == "Stripe");
        Assert.Contains(jobs, j => j.Company.Name == "Coinbase");
    }

    [Fact]
    public async Task GetJobByIdAsync_ExistingId_ShouldReturnCorrectJob()
    {
        // Act
        var job = await _aggregatorService.GetJobByIdAsync("hft-001");

        // Assert
        Assert.NotNull(job);
        Assert.Equal("Citadel Securities", job.Company.Name);
        Assert.Equal(FintechCategory.QuantAndAlgorithmicTrading, job.Category);
    }

    [Fact]
    public async Task GetJobByIdAsync_NonExistingId_ShouldReturnNull()
    {
        // Act
        var job = await _aggregatorService.GetJobByIdAsync("non-existent-id-999");

        // Assert
        Assert.Null(job);
    }

    [Fact]
    public async Task SubscribeJobAlertAsync_ValidEmail_ShouldReturnTrue()
    {
        // Arrange
        var alert = new JobAlertSubscription
        {
            Email = "quant.trader@fintech.io",
            Keyword = "C# .NET",
            Category = FintechCategory.QuantAndAlgorithmicTrading,
            MinSalary = 250000
        };

        // Act
        var result = await _aggregatorService.SubscribeJobAlertAsync(alert);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task SubscribeJobAlertAsync_InvalidEmail_ShouldReturnFalse()
    {
        // Arrange
        var alert = new JobAlertSubscription
        {
            Email = "",
            Keyword = "Rust"
        };

        // Act
        var result = await _aggregatorService.SubscribeJobAlertAsync(alert);

        // Assert
        Assert.False(result);
    }
}
