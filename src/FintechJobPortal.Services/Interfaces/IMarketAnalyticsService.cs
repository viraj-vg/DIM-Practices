using FintechJobPortal.Core.Models;

namespace FintechJobPortal.Services.Interfaces;

public interface IMarketAnalyticsService
{
    Task<MarketAnalytics> GetMarketIntelligenceAsync(CancellationToken cancellationToken = default);
}
