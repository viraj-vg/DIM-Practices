using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FintechJobPortal.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobAggregatorService _aggregatorService;
    private readonly ILogger<JobsController> _logger;

    public JobsController(IJobAggregatorService aggregatorService, ILogger<JobsController> logger)
    {
        _aggregatorService = aggregatorService;
        _logger = logger;
    }

    /// <summary>
    /// Searches and filters job listings across all aggregated fintech providers.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<JobFilterResult>> GetJobs([FromQuery] JobSearchQuery query, CancellationToken cancellationToken)
    {
        var result = await _aggregatorService.SearchJobsAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves full details of a specific job listing by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<JobListing>> GetJobById(string id, CancellationToken cancellationToken)
    {
        var job = await _aggregatorService.GetJobByIdAsync(id, cancellationToken);
        if (job == null)
        {
            return NotFound(new { message = $"Job listing with ID '{id}' was not found." });
        }
        return Ok(job);
    }

    /// <summary>
    /// Gets category breakdown taxonomy and counts.
    /// </summary>
    [HttpGet("categories")]
    public async Task<ActionResult<Dictionary<string, int>>> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await _aggregatorService.GetCategoryDistributionAsync(cancellationToken);
        return Ok(categories);
    }

    /// <summary>
    /// Subscribes to custom email alerts for matching fintech job postings.
    /// </summary>
    [HttpPost("alerts")]
    public async Task<ActionResult> SubscribeAlert([FromBody] JobAlertSubscription subscription, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(subscription.Email) || !subscription.Email.Contains('@'))
        {
            return BadRequest(new { message = "A valid email address is required to create a job alert." });
        }

        var success = await _aggregatorService.SubscribeJobAlertAsync(subscription, cancellationToken);
        if (success)
        {
            return Ok(new { message = "Job alert subscription created successfully!", subscriptionId = subscription.Id });
        }

        return StatusCode(500, new { message = "Failed to register job alert." });
    }

    /// <summary>
    /// Triggers an immediate refresh of aggregated job providers.
    /// </summary>
    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshCache(CancellationToken cancellationToken)
    {
        await _aggregatorService.RefreshJobCacheAsync(cancellationToken);
        return Ok(new { message = "Fintech job cache successfully refreshed from all upstream providers.", timestamp = DateTime.UtcNow });
    }
}
