using FintechJobPortal.Core.Enums;
using FintechJobPortal.Core.Models;
using FintechJobPortal.Services.Interfaces;

namespace FintechJobPortal.Services.Providers;

public class CryptoBlockchainProvider : IJobProvider
{
    public string ProviderName => "Web3, Crypto & Digital Asset Aggregator";
    public string SourceDomain => "cryptojobslist.com / web3.career";

    public Task<IReadOnlyList<JobListing>> FetchJobsAsync(CancellationToken cancellationToken = default)
    {
        var jobs = new List<JobListing>
        {
            new()
            {
                Id = "crypto-001",
                Title = "Senior Cryptographic & Institutional Custody Engineer",
                SourcePortal = "Coinbase Careers",
                ExternalUrl = "https://www.coinbase.com/careers",
                Company = new Company
                {
                    Name = "Coinbase",
                    Website = "https://coinbase.com",
                    Headquarters = "Remote-First (San Francisco, CA)",
                    Sector = "Digital Assets & Crypto Exchange",
                    CompanySize = "3,500+",
                    FundingStage = "Public (NASDAQ: COIN)",
                    GlassdoorRating = 4.3,
                    Overview = "Coinbase is building the cryptoeconomy—a more fair, accessible, efficient, and transparent financial system enabled by crypto."
                },
                Category = FintechCategory.BlockchainAndDigitalAssets,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Remote,
                Location = "Remote (Global / Americas / EMEA)",
                Country = "United States",
                MinSalary = 210000,
                MaxSalary = 330000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = true,
                BonusEstimate = "Substantial Equity grant + Crypto matching",
                Description = "Engineer institutional multi-party computation (MPC) key management, HSM cryptographic integrations, and cold storage signing infrastructure safeguarding billions in digital assets.",
                TechStack = new List<string> { "C#", "Go", "Rust", "Cryptography", "MPC", "AWS KMS", "Docker", "PostgreSQL" },
                KeyResponsibilities = new List<string>
                {
                    "Implement multi-party computation threshold signature schemes (TSS).",
                    "Build automated transaction approval engines with quorum verification.",
                    "Audit secure enclave (SGX / Nitro Enclaves) execution environments."
                },
                Qualifications = new List<string>
                {
                    "5+ years backend systems engineering with C#, Go, or Rust.",
                    "Strong background in applied cryptography (ECDSA, Ed25519, Zero Knowledge basics).",
                    "Experience with secure key lifecycle management and hardware security modules (HSM)."
                },
                Benefits = new List<string> { "100% Remote flexibility", "Crypto compensation options", "Generous wellness stipend", "Comprehensive global healthcare" },
                PostedDate = DateTime.UtcNow.AddDays(-1),
                IsFeatured = true,
                IsHotRole = true,
                ApplicantCount = 52
            },
            new()
            {
                Id = "crypto-002",
                Title = "DeFi Protocol & Smart Contract Security Engineer",
                SourcePortal = "Circle Talent Hub",
                ExternalUrl = "https://www.circle.com/careers",
                Company = new Company
                {
                    Name = "Circle",
                    Website = "https://circle.com",
                    Headquarters = "Boston, MA",
                    Sector = "Stablecoins & Programmable Money (USDC)",
                    CompanySize = "1,000+",
                    FundingStage = "Pre-IPO",
                    GlassdoorRating = 4.4,
                    Overview = "Circle is the issuer of USDC and EURC, leading global digital currency platforms powered by blockchain rails."
                },
                Category = FintechCategory.BlockchainAndDigitalAssets,
                ExperienceLevel = ExperienceLevel.Senior,
                LocationType = LocationType.Remote,
                Location = "Remote (US / EU)",
                Country = "United States",
                MinSalary = 195000,
                MaxSalary = 305000,
                Currency = "USD",
                HasEquity = true,
                HasBonus = false,
                BonusEstimate = "Stock Options / Token incentive",
                Description = "Architect Cross-Chain Transfer Protocol (CCTP) contracts and cross-network settlement modules for USDC on Ethereum, Solana, Arbitrum, and Cosmos.",
                TechStack = new List<string> { "Solidity", "Rust", "C#", "Foundry", "Hardhat", "Ethereum", "Solana" },
                KeyResponsibilities = new List<string>
                {
                    "Design and formally verify cross-chain message passing smart contracts.",
                    "Perform rigorous threat modeling and adversarial fuzz testing.",
                    "Collaborate with top tier audit firms on security certifications."
                },
                Qualifications = new List<string>
                {
                    "4+ years in smart contract development with Solidity / EVM and Rust / Solana.",
                    "Deep understanding of EVM opcode gas optimization and re-entrancy attack vectors.",
                    "Track record of deploying audited production smart contracts."
                },
                Benefits = new List<string> { "Remote-first workplace", "Flexible vacation policy", "Equipment budget ($2,500)", "Health, Dental & Vision" },
                PostedDate = DateTime.UtcNow.AddDays(-4),
                IsFeatured = false,
                IsHotRole = false,
                ApplicantCount = 34
            }
        };

        return Task.FromResult<IReadOnlyList<JobListing>>(jobs);
    }
}
