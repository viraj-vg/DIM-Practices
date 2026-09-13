using System.Collections.Concurrent;
using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FintechJobPortal.Services.Services;

public class JobAggregatorService : IJobAggregatorService
{
    private readonly IEnumerable<IJobProvider> _providers;
    private readonly ILogger<JobAggregatorService> _logger;
    private readonly List<JobAlertSubscription> _subscriptions = new();
    private readonly ReaderWriterLockSlim _cacheLock = new();
    private List<JobListing> _cachedJobs = new();
    private DateTime _lastRefreshTime = DateTime.MinValue;

    public JobAggregatorService(IEnumerable<IJobProvider> providers, ILogger<JobAggregatorService> logger)
    {
        _providers = providers;
        _logger = logger;
    }

    private async Task EnsureJobsLoadedAsync(CancellationToken cancellationToken)
    {
        if (_cachedJobs.Count > 0 && (DateTime.UtcNow - _lastRefreshTime).TotalMinutes < 30)
        {
            return;
        }

        await RefreshJobCacheAsync(cancellationToken);
    }

    public async Task RefreshJobCacheAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refreshing fintech job aggregation cache across {ProviderCount} providers...", _providers.Count());
        var aggregatedList = new List<JobListing>();

        foreach (var provider in _providers)
        {
            try
            {
                var jobs = await provider.FetchJobsAsync(cancellationToken);
                aggregatedList.AddRange(jobs);
                _logger.LogInformation("Provider '{ProviderName}' fetched {Count} jobs.", provider.ProviderName, jobs.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching jobs from provider '{ProviderName}'", provider.ProviderName);
            }
        }

        // Deduplicate jobs by unique combination of Company Name and Title
        var distinctJobs = aggregatedList
            .GroupBy(j => $"{j.Company.Name.Trim().ToLowerInvariant()}::{j.Title.Trim().ToLowerInvariant()}")
            .Select(g => g.First())
            .ToList();

        _cacheLock.EnterWriteLock();
        try
        {
            _cachedJobs = distinctJobs;
            _lastRefreshTime = DateTime.UtcNow;
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }

        _logger.LogInformation("Fintech job aggregation complete. Total cached jobs: {TotalJobs}", _cachedJobs.Count);
    }

    public async Task<IReadOnlyList<JobListing>> GetAllJobsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureJobsLoadedAsync(cancellationToken);
        _cacheLock.EnterReadLock();
        try
        {
            return _cachedJobs.ToList();
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public async Task<JobListing?> GetJobByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        await EnsureJobsLoadedAsync(cancellationToken);
        _cacheLock.EnterReadLock();
        try
        {
            return _cachedJobs.FirstOrDefault(j => string.Equals(j.Id, id, StringComparison.OrdinalIgnoreCase));
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public async Task<JobFilterResult> SearchJobsAsync(JobSearchQuery query, CancellationToken cancellationToken = default)
    {
        await EnsureJobsLoadedAsync(cancellationToken);

        _cacheLock.EnterReadLock();
        List<JobListing> allJobs;
        try
        {
            allJobs = _cachedJobs.ToList();
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }

        IEnumerable<JobListing> filtered = allJobs;

        // Keyword filter
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = query.Keyword.Trim();
            filtered = filtered.Where(j =>
                j.Title.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                j.Company.Name.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                j.Description.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                j.TechStack.Any(t => t.Contains(kw, StringComparison.OrdinalIgnoreCase)) ||
                j.Location.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                j.Company.Sector.Contains(kw, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Category filter
        if (query.Category.HasValue && query.Category.Value != FintechCategory.All)
        {
            filtered = filtered.Where(j => j.Category == query.Category.Value);
        }

        // Experience level filter
        if (query.ExperienceLevel.HasValue && query.ExperienceLevel.Value != ExperienceLevel.All)
        {
            filtered = filtered.Where(j => j.ExperienceLevel == query.ExperienceLevel.Value);
        }

        // Location type filter
        if (query.LocationType.HasValue && query.LocationType.Value != LocationType.All)
        {
            filtered = filtered.Where(j => j.LocationType == query.LocationType.Value);
        }

        // Location string filter
        if (!string.IsNullOrWhiteSpace(query.Location))
        {
            var loc = query.Location.Trim();
            filtered = filtered.Where(j =>
                j.Location.Contains(loc, StringComparison.OrdinalIgnoreCase) ||
                j.Country.Contains(loc, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Tech skill filter
        if (!string.IsNullOrWhiteSpace(query.TechSkill))
        {
            var skill = query.TechSkill.Trim();
            filtered = filtered.Where(j =>
                j.TechStack.Any(t => t.Contains(skill, StringComparison.OrdinalIgnoreCase))
            );
        }

        // Min salary filter
        if (query.MinSalary.HasValue && query.MinSalary.Value > 0)
        {
            filtered = filtered.Where(j => j.MaxSalary >= query.MinSalary.Value);
        }

        // Max salary filter
        if (query.MaxSalary.HasValue && query.MaxSalary.Value > 0)
        {
            filtered = filtered.Where(j => j.MinSalary <= query.MaxSalary.Value);
        }

        // Has Equity filter
        if (query.HasEquity.HasValue && query.HasEquity.Value)
        {
            filtered = filtered.Where(j => j.HasEquity);
        }

        // Source Portal filter
        if (!string.IsNullOrWhiteSpace(query.SourcePortal) && query.SourcePortal != "All")
        {
            filtered = filtered.Where(j => string.Equals(j.SourcePortal, query.SourcePortal, StringComparison.OrdinalIgnoreCase));
        }

        // Sorting
        filtered = query.SortBy?.ToLowerInvariant() switch
        {
            "salary_desc" => filtered.OrderByDescending(j => j.MaxSalary),
            "salary_asc" => filtered.OrderBy(j => j.MinSalary),
            "relevance" => filtered.OrderByDescending(j => j.IsFeatured).ThenByDescending(j => j.IsHotRole).ThenByDescending(j => j.ApplicantCount),
            _ => filtered.OrderByDescending(j => j.IsFeatured).ThenByDescending(j => j.PostedDate)
        };

        var filteredList = filtered.ToList();
        var totalCount = filteredList.Count;

        // Pagination
        var page = query.Page > 0 ? query.Page : 1;
        var pageSize = query.PageSize > 0 ? query.PageSize : 12;
        var paginatedJobs = filteredList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        // Calculate taxonomy & faceted counts
        var categoryCounts = allJobs
            .GroupBy(j => j.Category.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        var locationTypeCounts = allJobs
            .GroupBy(j => j.LocationType.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        var sourcePortalCounts = allJobs
            .GroupBy(j => j.SourcePortal)
            .ToDictionary(g => g.Key, g => g.Count());

        var topSkills = allJobs
            .SelectMany(j => j.TechStack)
            .GroupBy(s => s)
            .OrderByDescending(g => g.Count())
            .Take(12)
            .Select(g => g.Key)
            .ToList();

        var overallMin = allJobs.Count > 0 ? allJobs.Min(j => j.MinSalary) : 0;
        var overallMax = allJobs.Count > 0 ? allJobs.Max(j => j.MaxSalary) : 0;

        return new JobFilterResult
        {
            Jobs = paginatedJobs,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            CategoryCounts = categoryCounts,
            LocationTypeCounts = locationTypeCounts,
            SourcePortalCounts = sourcePortalCounts,
            TopAvailableSkills = topSkills,
            OverallMinSalary = overallMin,
            OverallMaxSalary = overallMax
        };
    }

    public async Task<Dictionary<string, int>> GetCategoryDistributionAsync(CancellationToken cancellationToken = default)
    {
        await EnsureJobsLoadedAsync(cancellationToken);
        _cacheLock.EnterReadLock();
        try
        {
            return _cachedJobs
                .GroupBy(j => j.Category.ToDisplayString())
                .ToDictionary(g => g.Key, g => g.Count());
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public Task<bool> SubscribeJobAlertAsync(JobAlertSubscription subscription, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(subscription.Email))
        {
            return Task.FromResult(false);
        }

        lock (_subscriptions)
        {
            _subscriptions.Add(subscription);
        }

        _logger.LogInformation("Registered job alert for {Email} with keyword '{Keyword}'", subscription.Email, subscription.Keyword);
        return Task.FromResult(true);
    }
}
