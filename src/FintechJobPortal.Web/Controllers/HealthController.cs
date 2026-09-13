using Microsoft.AspNetCore.Mvc;

namespace FintechJobPortal.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            service = "FinPulse Fintech Job Aggregator",
            version = "1.0.0"
        });
    }
}
