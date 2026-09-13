namespace FintechJobPortal.Core.Models;

public class JobFilterResult
{
    public List<JobListing> Jobs { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / (PageSize > 0 ? PageSize : 1));
    public Dictionary<string, int> CategoryCounts { get; set; } = new();
    public Dictionary<string, int> LocationTypeCounts { get; set; } = new();
    public Dictionary<string, int> SourcePortalCounts { get; set; } = new();
    public List<string> TopAvailableSkills { get; set; } = new();
    public decimal OverallMinSalary { get; set; }
    public decimal OverallMaxSalary { get; set; }
}
