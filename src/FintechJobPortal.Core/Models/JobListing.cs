using FintechJobPortal.Core.Enums;

namespace FintechJobPortal.Core.Models;

public class JobListing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string SourcePortal { get; set; } = string.Empty; // e.g. "Citadel Careers", "Stripe Talent", "CryptoJobsList", "eFinancialCareers"
    public string ExternalUrl { get; set; } = string.Empty;
    public Company Company { get; set; } = new();
    public FintechCategory Category { get; set; }
    public ExperienceLevel ExperienceLevel { get; set; }
    public LocationType LocationType { get; set; }
    public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;
    public string Location { get; set; } = string.Empty; // e.g., "New York, NY", "London, UK", "Remote"
    public string Country { get; set; } = string.Empty;
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public string Currency { get; set; } = "USD";
    public bool HasEquity { get; set; }
    public bool HasBonus { get; set; }
    public string BonusEstimate { get; set; } = string.Empty; // e.g. "Up to 100% bonus pool"
    public string Description { get; set; } = string.Empty;
    public List<string> TechStack { get; set; } = new();
    public List<string> KeyResponsibilities { get; set; } = new();
    public List<string> Qualifications { get; set; } = new();
    public List<string> Benefits { get; set; } = new();
    public DateTime PostedDate { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsHotRole { get; set; }
    public int ApplicantCount { get; set; }
}
