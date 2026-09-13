using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class PaymentsFintechProvider : IJobProvider
{
    public string ProviderName => "Global Payments & Open Banking Jobs";
    public string SourceDomain => "fintechfutures.com / paymentsjobs.com";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "pay-001",
                Title = "Staff Backend Engineer - Global Payment Rails & Settlements",
                SourcePortal = "Stripe Talent Network",
                ExternalUrl = "https://stripe.com/jobs",
                Company = new Company
                {
                    Name = "Stripe",
                    Website = "https://stripe.com",
                    Headquarters = "San Francisco, CA & Dublin, Ireland",
                    Sector = "Payment Infrastructure & Financial SaaS",
                    CompanySize = "7,000+",
                    FundingStage = "Late Stage / Pre-IPO",
                    GlassdoorRating = 4.5,
                    Overview = "Stripe is a financial infrastructure platform for businesses, powering billions in transactions globally."
                },
                Category = FintechCategory.PaymentsAndOpenBanking,
                ExperienceLevel = ExperienceLevel.LeadStaff,
                LocationType = LocationType.Remote,
                Location = "Remote (US & Canada)",
                Country = "United States",
                MinSalary = 230000,
                MaxSalary = 360000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Stock RSUs + Annual incentive",
                Description = "Scale Stripe's core financial ledger and settlement infrastructure. You will design idempotent, distributed transaction processing engines capable of five-nines (99.999%) availability during Black Friday peak volumes.",
                TechStack = new List<string> { "C#", ".NET 8", "Go", "Distributed Systems", "Kafka", "PostgreSQL", "Temporal", "Docker" },
                KeyResponsibilities = new List<string>
                {
                    "Architect multi-region payment routing engines with instant rollback guarantees.",
                    "Ensure absolute transactional consistency (ACID) and idempotency across global banking partners.",
                    "Mentor senior engineers and drive architectural roadmaps across payment protocols."
                },
                Qualifications = new List<string>
                {
                    "8+ years building high-scale distributed backend systems.",
                    "Deep expertise in transactional integrity, distributed locks, and event-driven architecture.",
                    "Familiarity with ISO 20022, SEPA, ACH, FedNow, or SWIFT protocols is a strong plus."
                },
                Benefits = new List<string> { "Remote-first culture & home office stipend", "Substantial Equity RSUs", "Comprehensive health coverage", "Unlimited PTO" },
                PostedDate = DateTime.UtcNow.AddDays(-3),
                IsFeatured = true,
                IsHotRole = true,
                ApplicantCount = 64
            },
            new()
            {
                Id = "pay-002",
                Title = "Senior Full-Stack Engineer - Open Banking API Platform",
                SourcePortal = "Plaid Careers",
                ExternalUrl = "https://plaid.com/careers/",
                Company = new Company
                {
                    Name = "Plaid",
                    Website = "https://plaid.com",
                    Headquarters = "San Francisco, CA",
                    Sector = "Open Banking & API Aggregation",
                    CompanySize = "1,000 - 5,000",
                    FundingStage = "Series D",
                    GlassdoorRating = 4.4,
                    Overview = "Plaid provides the technology platform that enables consumers to securely connect their financial accounts to apps."
                },
                Category = FintechCategory.PaymentsAndOpenBanking,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Hybrid,
                Location = "San Francisco, CA",
                Country = "United States",
                MinSalary = 190000,
                MaxSalary = 280000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Annual performance bonus + equity",
                Description = "Build consumer-facing Link interfaces and robust developer APIs that connect over 12,000 financial institutions to leading fintech applications.",
                TechStack = new List<string> { "TypeScript", "React", "C#", ".NET", "GraphQL", "AWS", "Redis" },
                KeyResponsibilities = new List<string>
                {
                    "Develop low-latency Open Banking authentication SDKs and developer portals.",
                    "Integrate OAuth2 / FDX banking standards for real-time account data syncing.",
                    "Optimize frontend bundle performance and accessibility."
                },
                Qualifications = new List<string>
                {
                    "5+ years developing modern web applications and API platforms.",
                    "Strong proficiency in React, TypeScript, and backend services (.NET or Node).",
                    "Understanding of security standards (OAuth 2.0, mTLS, PKCE)."
                },
                Benefits = new List<string> { "Equity Grant", "401(k) Match", "Parental Leave", "Wellness & Fitness Reimbursement" },
                PostedDate = DateTime.UtcNow.AddDays(-6),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 38
            },
            new()
            {
                Id = "pay-003",
                Title = "Payment Gateway Integration Specialist / .NET Engineer",
                SourcePortal = "Adyen Career Hub",
                ExternalUrl = "https://careers.adyen.com/",
                Company = new Company
                {
                    Name = "Adyen",
                    Website = "https://adyen.com",
                    Headquarters = "Amsterdam, Netherlands",
                    Sector = "Omnichannel Payments",
                    CompanySize = "4,000+",
                    FundingStage = "Public (AMS: ADYEN)",
                    GlassdoorRating = 4.5,
                    Overview = "Adyen is the financial technology platform of choice for leading companies including Spotify, Uber, and Microsoft."
                },
                Category = FintechCategory.PaymentsAndOpenBanking,
                ExperienceLevel = ExperienceLevel.MidLevel,
                LocationType = LocationType.Hybrid,
                Location = "Amsterdam, Netherlands / Frankfurt, Germany",
                Country = "Netherlands",
                MinSalary = 95000,
                MaxSalary = 145000,
                Currency = "EUR",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Company performance bonus",
                Description = "Expand our global acquiring engine connecting point-of-sale and e-commerce payment rails across 50+ countries.",
                TechStack = new List<string> { "C#", ".NET 8", "Java", "Kubernetes", "PostgreSQL", "REST APIs" },
                KeyResponsibilities = new List<string>
                {
                    "Build connectors for local card schemes, mobile wallets, and instant bank transfers.",
                    "Monitor global authorization rates and optimize smart retry mechanisms.",
                    "Implement PCI-DSS compliant tokenization protocols."
                },
                Qualifications = new List<string>
                {
                    "3-5 years experience in backend software engineering with C# or Java.",
                    "Familiarity with financial messaging formats and payment gateways.",
                    "Strong English communication skills."
                },
                Benefits = new List<string> { "Stock Option Plan", "Free daily lunch", "Commuter allowance", "30 days paid vacation" },
                PostedDate = DateTime.UtcNow.AddDays(-4),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 27
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
