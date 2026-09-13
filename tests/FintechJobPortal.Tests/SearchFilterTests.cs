using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using FintechJobPortal.Services.Providers;
using FintechJobPortal.Services.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace FintechJobPortal.Tests;

public class SearchFilterTests
{
    private readonly IJobAggregatorService _aggregatorService;

    public SearchFilterTests()
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
    public async Task SearchJobsAsync_WithKeywordCSharp_ShouldReturnMatchingRoles()
    {
        // Arrange
        var query = new JobSearchQuery { Keyword = "C#" };

        // Act
        var result = await _aggregatorService.SearchJobsAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Jobs.Count > 0);
        Assert.All(result.Jobs, job =>
        {
            var matches = job.Title.Contains("C#", StringComparison.OrdinalIgnoreCase) ||
                          job.Description.Contains("C#", StringComparison.OrdinalIgnoreCase) ||
                          job.TechStack.Any(t => t.Contains("C#", StringComparison.OrdinalIgnoreCase));
            Assert.True(matches, $"Job {job.Title} should match keyword C#");
        });
    }

    [Fact]
    public async Task SearchJobsAsync_FilterByCategory_ShouldReturnOnlyCategoryJobs()
    {
        // Arrange
        var query = new JobSearchQuery
        {
            Category = FintechCategory.BlockchainAndDigitalAssets
        };

        // Act
        var result = await _aggregatorService.SearchJobsAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Jobs.Count > 0);
        Assert.All(result.Jobs, job =>
        {
            Assert.Equal(FintechCategory.BlockchainAndDigitalAssets, job.Category);
        });
    }

    [Fact]
    public async Task SearchJobsAsync_FilterByRemoteLocationType_ShouldReturnOnlyRemoteJobs()
    {
        // Arrange
        var query = new JobSearchQuery
        {
            LocationType = LocationType.Remote
        };

        // Act
        var result = await _aggregatorService.SearchJobsAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Jobs.Count > 0);
        Assert.All(result.Jobs, job =>
        {
            Assert.Equal(LocationType.Remote, job.LocationType);
        });
    }

    [Fact]
    public async Task SearchJobsAsync_FilterByMinSalary_ShouldFilterCorrectly()
    {
        // Arrange
        var minSalaryThreshold = 250000m;
        var query = new JobSearchQuery
        {
            MinSalary = minSalaryThreshold
        };

        // Act
        var result = await _aggregatorService.SearchJobsAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.All(result.Jobs, job =>
        {
            Assert.True(job.MaxSalary >= minSalaryThreshold, $"Expected max salary >= {minSalaryThreshold}, got {job.MaxSalary}");
        });
    }

    [Fact]
    public async Task SearchJobsAsync_SortBySalaryDesc_ShouldBeDescending()
    {
        // Arrange
        var query = new JobSearchQuery
        {
            SortBy = "salary_desc",
            PageSize = 20
        };

        // Act
        var result = await _aggregatorService.SearchJobsAsync(query);

        // Assert
        Assert.NotNull(result);
        for (int i = 0; i < result.Jobs.Count - 1; i++)
        {
            Assert.True(result.Jobs[i].MaxSalary >= result.Jobs[i + 1].MaxSalary, "Jobs should be ordered descending by MaxSalary");
        }
    }
}
