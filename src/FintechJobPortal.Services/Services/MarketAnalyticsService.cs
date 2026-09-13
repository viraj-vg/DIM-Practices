using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Services;

public class MarketAnalyticsService : IMarketAnalyticsService
{
    private readonly IJobAggregatorService _aggregatorService;

    public MarketAnalyticsService(IJobAggregatorService aggregatorService)
    {
        _aggregatorService = aggregatorService;
    }

    public async Task<MarketAnalytics> GetMarketIntelligenceAsync(CancellationToken cancellationToken = default)
    {
        var allJobs = await _aggregatorService.GetAllJobsAsync(cancellationToken);

        if (allJobs.Count == 0)
        {
            return new MarketAnalytics();
        }

        var totalJobs = allJobs.Count;
        var avgSalary = allJobs.Average(j => (j.MinSalary + j.MaxSalary) / 2);
        var maxSalary = allJobs.Max(j => j.MaxSalary);
        var remoteCount = allJobs.Count(j => j.LocationType == LocationType.Remote);
        var remotePercentage = Math.Round((double)remoteCount / totalJobs * 100, 1);
        var uniqueCompanies = allJobs.Select(j => j.Company.Name).Distinct().Count();

        // Sector breakdown
        var categorySalaries = allJobs
            .GroupBy(j => j.Category)
            .Select(g => new CategorySalaryMetric
            {
                CategoryName = g.Key.ToDisplayString(),
                AvgMinSalary = Math.Round(g.Average(j => j.MinSalary)),
                AvgMaxSalary = Math.Round(g.Average(j => j.MaxSalary)),
                OpeningsCount = g.Count()
            })
            .OrderByDescending(c => c.AvgMaxSalary)
            .ToList();

        // Top skills & demand
        var topSkills = allJobs
            .SelectMany(j => j.TechStack.Select(skill => new { Skill = skill, MidSalary = (j.MinSalary + j.MaxSalary) / 2 }))
            .GroupBy(x => x.Skill)
            .Select(g => new SkillDemandMetric
            {
                Skill = g.Key,
                JobCount = g.Count(),
                AverageSalary = Math.Round(g.Average(x => x.MidSalary))
            })
            .OrderByDescending(s => s.JobCount)
            .ThenByDescending(s => s.AverageSalary)
            .Take(10)
            .ToList();

        // Top hiring hubs
        var hubEmojis = new Dictionary<string, string>
        {
            { "New York", "🇺🇸" },
            { "London", "🇬🇧" },
            { "San Francisco", "🇺🇸" },
            { "Amsterdam", "🇳🇱" },
            { "Frankfurt", "🇩🇪" },
            { "Remote", "🌐" }
        };

        var hiringHubs = allJobs
            .GroupBy(j => ExtractHub(j.Location))
            .Select(g => new LocationDemandMetric
            {
                Hub = g.Key,
                OpeningsCount = g.Count(),
                FlagEmoji = hubEmojis.TryGetValue(g.Key, out var emoji) ? emoji : "📍"
            })
            .OrderByDescending(h => h.OpeningsCount)
            .ToList();

        // Top hiring companies
        var topCompanies = allJobs
            .GroupBy(j => j.Company.Name)
            .Select(g => new CompanyHiringMetric
            {
                CompanyName = g.Key,
                Sector = g.First().Company.Sector,
                OpeningsCount = g.Count(),
                Rating = g.First().Company.GlassdoorRating
            })
            .OrderByDescending(c => c.OpeningsCount)
            .ThenByDescending(c => c.Rating)
            .Take(6)
            .ToList();

        return new MarketAnalytics
        {
            TotalOpenings = totalJobs,
            AverageSalary = Math.Round(avgSalary),
            HighestSalary = maxSalary,
            RemotePercentage = remotePercentage,
            ActiveFintechCompanies = uniqueCompanies,
            CategorySalaries = categorySalaries,
            TopSkills = topSkills,
            TopHiringHubs = hiringHubs,
            TopHiringCompanies = topCompanies,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private static string ExtractHub(string location)
    {
        if (string.IsNullOrWhiteSpace(location)) return "Global";
        if (location.Contains("Remote", StringComparison.OrdinalIgnoreCase)) return "Remote";
        if (location.Contains("New York", StringComparison.OrdinalIgnoreCase)) return "New York";
        if (location.Contains("London", StringComparison.OrdinalIgnoreCase)) return "London";
        if (location.Contains("San Francisco", StringComparison.OrdinalIgnoreCase) || location.Contains("Palo Alto", StringComparison.OrdinalIgnoreCase)) return "San Francisco";
        if (location.Contains("Amsterdam", StringComparison.OrdinalIgnoreCase)) return "Amsterdam";
        if (location.Contains("Frankfurt", StringComparison.OrdinalIgnoreCase)) return "Frankfurt";
        return location.Split(',')[0].Trim();
    }
}
