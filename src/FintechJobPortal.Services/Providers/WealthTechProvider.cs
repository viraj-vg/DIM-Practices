using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class WealthTechProvider : IJobProvider
{
    public string ProviderName => "WealthTech & Brokerage Hub";
    public string SourceDomain => "wealthtechweekly.com / efinancialcareers.com";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "wlt-001",
                Title = "Principal Engineer - Real-Time Portfolio & Tax-Loss Harvesting Engine",
                SourcePortal = "Wealthfront Talent",
                ExternalUrl = "https://www.wealthfront.com/careers",
                Company = new Company
                {
                    Name = "Wealthfront",
                    Website = "https://wealthfront.com",
                    Headquarters = "Palo Alto, CA",
                    Sector = "Automated Wealth Management & Robo-Advisory",
                    CompanySize = "500 - 1,000",
                    FundingStage = "Late Stage",
                    GlassdoorRating = 4.5,
                    Overview = "Wealthfront provides automated wealth management, helping users invest, save, and build long-term financial security."
                },
                Category = FintechCategory.WealthTechAndRoboAdvisory,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Hybrid,
                Location = "San Francisco, CA / Palo Alto, CA",
                Country = "United States",
                MinSalary = 240000,
                MaxSalary = 370000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Substantial equity grant + performance bonus",
                Description = "Lead the engineering of automated index replication, direct indexing, and real-time algorithmic tax-loss harvesting engines managing over $60B+ in AUM.",
                TechStack = new List<string> { "C#", ".NET 8", "Java", "Python", "PostgreSQL", "Kafka", "Redis", "AWS" },
                KeyResponsibilities = new List<string>
                {
                    "Architect continuous portfolio rebalancing algorithms considering transaction costs and capital gains taxes.",
                    "Optimize batch and real-time trade generation pipelines.",
                    "Set architectural standards and mentor senior staff across wealthtech squads."
                },
                Qualifications = new List<string>
                {
                    "8+ years of software engineering experience.",
                    "Strong background in algorithms, financial mathematics, or portfolio optimization theory.",
                    "Demonstrated leadership on high-reliability financial systems."
                },
                Benefits = new List<string> { "Substantial Equity", "Full medical/dental/vision coverage", "401(k) Match", "Competitive fertility & family planning benefits" },
                PostedDate = DateTime.UtcNow.AddDays(-3),
                IsFeatured = true,
                IsHotRole = false,
                ApplicantCount = 31
            },
            new()
            {
                Id = "wlt-002",
                Title = "Senior C# .NET Developer - Real-Time Market Data Terminal",
                SourcePortal = "Bloomberg Engineering Careers",
                ExternalUrl = "https://www.bloomberg.com/careers/",
                Company = new Company
                {
                    Name = "Bloomberg L.P.",
                    Website = "https://bloomberg.com",
                    Headquarters = "New York, NY",
                    Sector = "Financial Data & Analytics",
                    CompanySize = "20,000+",
                    FundingStage = "Private",
                    GlassdoorRating = 4.4,
                    Overview = "Bloomberg connects decision makers to a dynamic network of data, people and ideas in global financial markets."
                },
                Category = FintechCategory.WealthTechAndRoboAdvisory,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Hybrid,
                Location = "New York, NY",
                Country = "United States",
                MinSalary = 190000,
                MaxSalary = 295000,
                Currency = "USD",
                HasEquity = false,
                HasBonus = true,
                BonusEstimate = "Discretionary cash bonus ($40k - $80k)",
                Description = "Build low-latency market data processing engines delivering real-time quotes, news feeds, and charting analytics to over 350,000 financial terminals worldwide.",
                TechStack = new List<string> { "C#", ".NET", "C++", "gRPC", "TypeScript", "Microservices", "Kafka" },
                KeyResponsibilities = new List<string>
                {
                    "Develop high-throughput streaming feeds for equities, fixed income, and FX instruments.",
                    "Build developer SDKs and analytics calculation components.",
                    "Collaborate with UX and financial quantitative specialists."
                },
                Qualifications = new List<string>
                {
                    "5+ years professional C# / .NET or C++ experience.",
                    "Deep understanding of concurrent programming, garbage collection tuning, and network protocols.",
                    "BS/MS in Computer Science or related technical discipline."
                },
                Benefits = new List<string> { "Comprehensive healthcare", "Exceptional 401(k) match (up to 85% on 6%)", "Subsidized cafeteria & snacks", "Tuition assistance" },
                PostedDate = DateTime.UtcNow.AddDays(-7),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 49
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
