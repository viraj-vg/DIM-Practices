using FintechJobPortal.Core.Enums;

namespace FintechJobPortal.Core.Models;

public class JobSearchQuery
{
    public string? Keyword { get; set; }
    public FintechCategory? Category { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
    public LocationType? LocationType { get; set; }
    public string? Location { get; set; }
    public string? TechSkill { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public bool? HasEquity { get; set; }
    public string? SourcePortal { get; set; }
    public string SortBy { get; set; } = "newest"; // "newest", "salary_desc", "salary_asc", "relevance"
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}
