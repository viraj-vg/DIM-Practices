using FintechJobPortal.Core.Models;

namespace FintechJobPortal.Services.Interfaces;

public interface IJobAggregatorService
{
    Task<JobFilterResult> SearchJobsAsync(JobSearchQuery query, CancellationToken cancellationToken = default);
    Task<JobListing?> GetJobByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JobListing>> GetAllJobsAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, int>> GetCategoryDistributionAsync(CancellationToken cancellationToken = default);
    Task<bool> SubscribeJobAlertAsync(JobAlertSubscription subscription, CancellationToken cancellationToken = default);
    Task RefreshJobCacheAsync(CancellationToken cancellationToken = default);
}
