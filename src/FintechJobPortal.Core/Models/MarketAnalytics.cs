namespace FintechJobPortal.Core.Models;

public class MarketAnalytics
{
    public int TotalOpenings { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal HighestSalary { get; set; }
    public double RemotePercentage { get; set; }
    public int ActiveFintechCompanies { get; set; }
    public List<CategorySalaryMetric> CategorySalaries { get; set; } = new();
    public List<SkillDemandMetric> TopSkills { get; set; } = new();
    public List<LocationDemandMetric> TopHiringHubs { get; set; } = new();
    public List<CompanyHiringMetric> TopHiringCompanies { get; set; } = new();
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}

public class CategorySalaryMetric
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal AvgMinSalary { get; set; }
    public decimal AvgMaxSalary { get; set; }
    public int OpeningsCount { get; set; }
}

public class SkillDemandMetric
{
    public string Skill { get; set; } = string.Empty;
    public int JobCount { get; set; }
    public decimal AverageSalary { get; set; }
}

public class LocationDemandMetric
{
    public string Hub { get; set; } = string.Empty;
    public int OpeningsCount { get; set; }
    public string FlagEmoji { get; set; } = string.Empty;
}

public class CompanyHiringMetric
{
    public string CompanyName { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public int OpeningsCount { get; set; }
    public double Rating { get; set; }
}
