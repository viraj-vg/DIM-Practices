using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class NeoBankingProvider : IJobProvider
{
    public string ProviderName => "Global Neo-Banking & Challenger Banks";
    public string SourceDomain => "neobankjobs.com / linkedin.com/jobs";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "neo-001",
                Title = "Lead Core Banking Platform Engineer (.NET 8 Microservices)",
                SourcePortal = "Revolut Careers",
                ExternalUrl = "https://www.revolut.com/careers/",
                Company = new Company
                {
                    Name = "Revolut",
                    Website = "https://revolut.com",
                    Headquarters = "London, UK",
                    Sector = "Digital Banking & Super App",
                    CompanySize = "10,000+",
                    FundingStage = "Late Stage ($45B Valuation)",
                    GlassdoorRating = 4.2,
                    Overview = "Revolut is building the world's first truly global financial superapp with over 45 million retail customers."
                },
                Category = FintechCategory.NeoBankingAndCoreBanking,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Hybrid,
                Location = "London, UK / Kraków, Poland",
                Country = "United Kingdom",
                MinSalary = 130000,
                MaxSalary = 190000,
                Currency = "GBP",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Substantial equity stock grant",
                Description = "Drive the technical architecture of Revolut's real-time core account ledger, treasury reconciliation engine, and multi-currency FX balance engines.",
                TechStack = new List<string> { "C#", ".NET 8", "Java", "Kubernetes", "Kafka", "PostgreSQL", "Docker", "GCP" },
                KeyResponsibilities = new List<string>
                {
                    "Lead domain-driven design (DDD) modeling of multi-currency balance accounts.",
                    "Optimize high-throughput Kafka event streaming for millions of real-time push events.",
                    "Ensure fault-tolerant banking compliance with FCA / European Central Bank standards."
                },
                Qualifications = new List<string>
                {
                    "7+ years experience in backend architecture and enterprise microservices.",
                    "High proficiency in C# .NET or JVM ecosystem.",
                    "Experience designing double-entry bookkeeping ledgers or high-volume payment routing."
                },
                Benefits = new List<string> { "Equity Options", "Private medical insurance", "Latest MacBook Pro M3 Max", "Revolut Metal membership free" },
                PostedDate = DateTime.UtcNow.AddDays(-2),
                IsFeatured = true,
                IsHotRole = true,
                ApplicantCount = 71
            },
            new()
            {
                Id = "neo-002",
                Title = "Senior Distributed Systems Engineer - Card Issuing & Authorization",
                SourcePortal = "Monzo Bank Careers",
                ExternalUrl = "https://monzo.com/careers/",
                Company = new Company
                {
                    Name = "Monzo Bank",
                    Website = "https://monzo.com",
                    Headquarters = "London, UK",
                    Sector = "Regulated Digital Banking",
                    CompanySize = "3,000+",
                    FundingStage = "Pre-IPO",
                    GlassdoorRating = 4.6,
                    Overview = "Monzo is making money work for everyone with award-winning mobile banking in the UK and US."
                },
                Category = FintechCategory.NeoBankingAndCoreBanking,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Remote,
                Location = "Remote (UK / Europe)",
                Country = "United Kingdom",
                MinSalary = 115000,
                MaxSalary = 165000,
                Currency = "GBP",
                HasEquity = true,
                HasBonus = false,
                BonusEstimate = "Monzo Share Options Scheme",
                Description = "Maintain sub-50ms response latency for Mastercard authorizations, real-time balance calculations, and fraud screening algorithms.",
                TechStack = new List<string> { "Go", "C#", "Kubernetes", "Envoy", "Cassandra", "Microservices", "Prometheus" },
                KeyResponsibilities = new List<string>
                {
                    "Develop and operate hundreds of microservices orchestrating card transactions.",
                    "Implement chaos engineering and automated failover recovery mechanisms.",
                    "Scale internal ledger databases across multiple AWS availability zones."
                },
                Qualifications = new List<string>
                {
                    "5+ years engineering reliable distributed systems in Go or C#.",
                    "Hands-on experience with Kubernetes, gRPC, and distributed tracing.",
                    "Strong debugging and incident management abilities."
                },
                Benefits = new List<string> { "Remote work freedom", "Share options", "£1,000 annual learning budget", "28 days holiday + bank holidays" },
                PostedDate = DateTime.UtcNow.AddDays(-5),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 45
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
