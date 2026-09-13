using FintechJobPortal.Core.Enums;

namespace FintechJobPortal.Core.Models;

public class JobAlertSubscription
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string? Keyword { get; set; }
    public FintechCategory? Category { get; set; }
    public decimal? MinSalary { get; set; }
    public LocationType? LocationType { get; set; }
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
