using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FintechJobPortal.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IMarketAnalyticsService _analyticsService;

    public AnalyticsController(IMarketAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Returns global fintech compensation benchmarks, skill demand rankings, and hiring hubs.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<MarketAnalytics>> GetMarketIntelligence(CancellationToken cancellationToken)
    {
        var analytics = await _analyticsService.GetMarketIntelligenceAsync(cancellationToken);
        return Ok(analytics);
    }
}
