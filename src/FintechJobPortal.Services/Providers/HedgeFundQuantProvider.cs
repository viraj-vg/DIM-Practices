using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class HedgeFundQuantProvider : IJobProvider
{
    public string ProviderName => "Wall Street Quant & HFT Aggregator";
    public string SourceDomain => "efinancialcareers.com / hedgefundjobs.io";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "hft-001",
                Title = "Lead Low-Latency C# / C++ Quantitative Trading Engineer",
                SourcePortal = "Citadel Securities Careers",
                ExternalUrl = "https://www.citadelsecurities.com/careers/",
                Company = new Company
                {
                    Name = "Citadel Securities",
                    Website = "https://citadelsecurities.com",
                    Headquarters = "Miami, FL & New York, NY",
                    Sector = "Market Making & Quantitative Trading",
                    CompanySize = "1,000 - 5,000",
                    FundingStage = "Private / Market Maker",
                    GlassdoorRating = 4.7,
                    Overview = "Citadel Securities is a leading global market maker across a broad array of fixed income and equity products."
                },
                Category = FintechCategory.QuantAndAlgorithmicTrading,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Hybrid,
                Location = "New York, NY",
                Country = "United States",
                MinSalary = 320000,
                MaxSalary = 550000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Discretionary performance bonus (50% - 150%)",
                Description = "Architect ultra-low latency execution engines, market data gateways, and real-time order routing algorithms processing millions of orders per second. You will optimize kernel bypass networking, cache-locality, and concurrent lock-free data structures.",
                TechStack = new List<string> { "C#", ".NET 8", "C++20", "Linux Kernel", "FPGA", "ZeroMQ", "Fix Protocol", "Low Latency" },
                KeyResponsibilities = new List<string>
                {
                    "Design sub-microsecond trading engines handling global exchange connectivity.",
                    "Collaborate directly with Quantitative Researchers and Portfolio Managers on execution strategy deployment.",
                    "Optimize memory layouts, SIMD vectorization, and lock-free concurrency.",
                    "Conduct post-trade latency profile analytics and continuous throughput tuning."
                },
                Qualifications = new List<string>
                {
                    "7+ years of experience with C# / .NET performance tuning or C++.",
                    "Deep knowledge of multithreading, memory management, garbage collection optimization, and lock-free queues.",
                    "Experience with socket programming, TCP/UDP multicast, and financial protocols (FIX, ITCH, OUCH).",
                    "BS/MS in Computer Science, Computer Engineering, or related quantitative field."
                },
                Benefits = new List<string> { "Top 1% Total Compensation", "Comprehensive Health/Dental/Vision", "Generous 401(k) Match", "Catered Gourmet Meals", "Relocation Support" },
                PostedDate = DateTime.UtcNow.AddDays(-2),
                IsFeatured = true,
                IsHotRole = true,
                ApplicantCount = 43
            },
            new()
            {
                Id = "hft-002",
                Title = "Senior Quantitative Developer - Automated Alpha Strategies",
                SourcePortal = "Two Sigma Careers",
                ExternalUrl = "https://www.twosigma.com/careers/",
                Company = new Company
                {
                    Name = "Two Sigma",
                    Website = "https://twosigma.com",
                    Headquarters = "New York, NY",
                    Sector = "Quantitative Hedge Fund & AI",
                    CompanySize = "1,000 - 5,000",
                    FundingStage = "Hedge Fund",
                    GlassdoorRating = 4.6,
                    Overview = "Two Sigma is a financial sciences company combining massive data sets with rigorous mathematical modeling to predict market movements."
                },
                Category = FintechCategory.QuantAndAlgorithmicTrading,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Hybrid,
                Location = "New York, NY",
                Country = "United States",
                MinSalary = 260000,
                MaxSalary = 440000,
                Currency = "USD",
                HasEquity = false,
                HasBonus = true,
                BonusEstimate = "Substantial annual performance pool",
                Description = "Join our Alpha Generation team to translate complex econometric and machine learning research into high-throughput production trading pipelines.",
                TechStack = new List<string> { "C#", "Python", ".NET", "Kafka", "PostgreSQL", "AWS", "PyTorch", "TimescaleDB" },
                KeyResponsibilities = new List<string>
                {
                    "Build distributed backtesting frameworks processing terabytes of market tick data.",
                    "Deploy automated risk limits and position tracking systems in real time.",
                    "Maintain high data integrity across global multi-asset class pipelines."
                },
                Qualifications = new List<string>
                {
                    "5+ years engineering experience with strong C# / .NET or Python foundations.",
                    "Solid understanding of statistical models, time-series analysis, and algorithmic trading.",
                    "Strong background in relational and time-series databases."
                },
                Benefits = new List<string> { "Premier Medical Coverage", "Annual Wellness Stipend", "Learning & Development Fund", "401(k) 100% Match up to 6%" },
                PostedDate = DateTime.UtcNow.AddDays(-5),
                IsFeatured = true,
                IsHotRole = false,
                ApplicantCount = 29
            },
            new()
            {
                Id = "hft-003",
                Title = "Junior / Graduate Quantitative Software Engineer",
                SourcePortal = "Jane Street Careers",
                ExternalUrl = "https://www.janestreet.com/join-jane-street/",
                Company = new Company
                {
                    Name = "Jane Street",
                    Website = "https://janestreet.com",
                    Headquarters = "New York, London, Hong Kong",
                    Sector = "Proprietary Trading & Market Making",
                    CompanySize = "2,000+",
                    FundingStage = "Private",
                    GlassdoorRating = 4.8,
                    Overview = "Jane Street is a quantitative trading firm that focuses on tech, collaboration, and finding smart solutions to difficult problems."
                },
                Category = FintechCategory.QuantAndAlgorithmicTrading,
                ExperienceLevel = ExperienceLevel.EntryLevel,
                LocationType = LocationType.OnSite,
                Location = "London, UK",
                Country = "United Kingdom",
                MinSalary = 140000,
                MaxSalary = 220000,
                Currency = "GBP",
                HasEquity = false,
                HasBonus = true,
                BonusEstimate = "Target bonus £40,000+",
                Description = "Work alongside world-class engineers and traders building mission-critical trading infrastructure, real-time risk monitors, and statistical arbitrage tools.",
                TechStack = new List<string> { "C#", "OCaml", "C++", "Linux", "Algorithms", "Data Structures" },
                KeyResponsibilities = new List<string>
                {
                    "Write robust, testable code for trading floor operations.",
                    "Analyze live execution telemetry and debug production edge cases.",
                    "Participate in daily trading desk syncs and market simulations."
                },
                Qualifications = new List<string>
                {
                    "Degree in Computer Science, Mathematics, Physics, or related discipline.",
                    "Exceptional algorithmic problem-solving and clean coding skills.",
                    "Passion for financial markets and distributed computing."
                },
                Benefits = new List<string> { "Free Breakfast & Lunch", "Private Healthcare", "Gym Membership", "Generous Pension Contribution" },
                PostedDate = DateTime.UtcNow.AddDays(-1),
                IsFeatured = false,
                IsHotRole = true,
                ApplicantCount = 82
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
