using FintechJobPortal.Core.Models;

namespace FintechJobPortal.Services.Interfaces;

public interface IJobProvider
{
    string ProviderName { get; }
    string SourceDomain { get; }
    Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default);
}
