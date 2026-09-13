using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class InsurTechRiskProvider : IJobProvider
{
    public string ProviderName => "Fintech Risk, AI & InsurTech Network";
    public string SourceDomain => "cyberfintechjobs.org / risktech-forum.com";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "risk-001",
                Title = "Staff Machine Learning Engineer - Anti-Money Laundering (AML) & Graph Neural Networks",
                SourcePortal = "Chainalysis Careers",
                ExternalUrl = "https://www.chainalysis.com/careers/",
                Company = new Company
                {
                    Name = "Chainalysis",
                    Website = "https://chainalysis.com",
                    Headquarters = "New York, NY",
                    Sector = "Blockchain Intelligence & Financial Crime Compliance",
                    CompanySize = "1,000+",
                    FundingStage = "Series F ($8.6B Valuation)",
                    GlassdoorRating = 4.3,
                    Overview = "Chainalysis is the blockchain data platform providing data, software, services, and research to government agencies, exchanges, financial institutions, and insurance companies."
                },
                Category = FintechCategory.RiskAIAndFraudDetection,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Remote,
                Location = "Remote (US / UK / Singapore)",
                Country = "United States",
                MinSalary = 225000,
                MaxSalary = 345000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Equity shares + performance bonus",
                Description = "Train large-scale Graph Neural Networks (GNNs) and graph clustering algorithms across multi-billion node transaction graphs to detect sanctions evasion, illicit fund routing, and ransomware movements in real time.",
                TechStack = new List<string> { "Python", "PyTorch", "C#", ".NET", "Spark", "Neo4j", "Kafka", "AWS" },
                KeyResponsibilities = new List<string>
                {
                    "Scale graph entity attribution algorithms on petabyte-scale on-chain datasets.",
                    "Develop low-latency real-time scoring microservices for financial risk screening.",
                    "Publish peer-reviewed research and collaborate with national enforcement agencies."
                },
                Qualifications = new List<string>
                {
                    "7+ years in machine learning engineering with emphasis on graph embeddings / anomaly detection.",
                    "Strong proficiency in PyTorch/TensorFlow and production backend integration in C# or Python.",
                    "Solid understanding of AML regulations and blockchain heuristics."
                },
                Benefits = new List<string> { "100% Remote flexibility", "Equity Stock Options", "Unlimited PTO", "Home Office Setup $1,500" },
                PostedDate = DateTime.UtcNow.AddDays(-2),
                IsFeatured = true,
                IsHotRole = true,
                ApplicantCount = 37
            },
            new()
            {
                Id = "risk-002",
                Title = "Senior Site Reliability & Cloud Infrastructure Engineer (Fintech SRE)",
                SourcePortal = "Robinhood Talent",
                ExternalUrl = "https://robinhood.com/careers",
                Company = new Company
                {
                    Name = "Robinhood",
                    Website = "https://robinhood.com",
                    Headquarters = "Menlo Park, CA",
                    Sector = "Retail Brokerage & Crypto",
                    CompanySize = "3,000+",
                    FundingStage = "Public (NASDAQ: HOOD)",
                    GlassdoorRating = 4.2,
                    Overview = "Robinhood's mission is to democratize finance for all with commission-free trading in stocks, options, ETFs, and crypto."
                },
                Category = FintechCategory.FintechDevOpsAndCloud,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Hybrid,
                Location = "Menlo Park, CA / Denver, CO",
                Country = "United States",
                MinSalary = 185000,
                MaxSalary = 275000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Annual equity refresher + target bonus",
                Description = "Architect high-availability Kubernetes clusters, multi-region database failovers, and Terraform infrastructure-as-code supporting massive market volatility spikes during market open.",
                TechStack = new List<string> { "Kubernetes", "Terraform", "AWS", "Docker", "Go", "C#", "Prometheus", "Grafana", "Istio" },
                KeyResponsibilities = new List<string>
                {
                    "Ensure 99.99% uptime during extreme market trading volume surges.",
                    "Automate self-healing zero-downtime deployment pipelines with GitOps (ArgoCD).",
                    "Drive chaos experiments and game-day disaster recovery simulations."
                },
                Qualifications = new List<string>
                {
                    "5+ years in SRE / DevOps roles managing enterprise cloud infrastructure.",
                    "Deep mastery of Kubernetes, AWS networking, VPC peering, and Terraform.",
                    "Experience with observability stacks (Prometheus, Datadog, OpenTelemetry)."
                },
                Benefits = new List<string> { "Stock Grants (RSUs)", "Comprehensive Health, Vision, Dental", "401(k) match", "Flexible time off" },
                PostedDate = DateTime.UtcNow.AddDays(-3),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 41
            },
            new()
            {
                Id = "risk-003",
                Title = "Lead InsurTech Systems Architect - Autonomous Claims Underwriting",
                SourcePortal = "Lemonade Insurance Careers",
                ExternalUrl = "https://www.lemonade.com/careers",
                Company = new Company
                {
                    Name = "Lemonade",
                    Website = "https://lemonade.com",
                    Headquarters = "New York, NY",
                    Sector = "AI-Driven InsurTech",
                    CompanySize = "1,500+",
                    FundingStage = "Public (NYSE: LMND)",
                    GlassdoorRating = 4.3,
                    Overview = "Lemonade is using artificial intelligence and behavioral economics to reinvent insurance."
                },
                Category = FintechCategory.InsurTech,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Hybrid,
                Location = "New York, NY / Tel Aviv",
                Country = "United States",
                MinSalary = 205000,
                MaxSalary = 310000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = false,
                BonusEstimate = "Public stock equity RSUs",
                Description = "Lead the design of AI Maya & AI Jim autonomous conversational and algorithmic claims settling engines that pay out claims in seconds.",
                TechStack = new List<string> { "C#", ".NET 8", "Python", "PostgreSQL", "Kafka", "Docker", "AWS" },
                KeyResponsibilities = new List<string>
                {
                    "Architect policy generation and dynamic risk calculation pipelines.",
                    "Integrate real-time geospatial and biometric fraud detection systems.",
                    "Oversee high-throughput microservices architecture."
                },
                Qualifications = new List<string>
                {
                    "7+ years backend development in C# / .NET or modern Python/Go frameworks.",
                    "Strong background in API design, microservices, and asynchronous event streams.",
                    "Experience in InsurTech or regulated financial products."
                },
                Benefits = new List<string> { "Equity RSUs", "Full health coverage", "Flexible working model", "Parental leave" },
                PostedDate = DateTime.UtcNow.AddDays(-6),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 28
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
