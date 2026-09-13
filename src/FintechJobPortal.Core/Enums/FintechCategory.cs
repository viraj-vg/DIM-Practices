namespace FintechJobPortal.Core.Enums;

public enum FintechCategory
{
    All,
    QuantAndAlgorithmicTrading,
    PaymentsAndOpenBanking,
    NeoBankingAndCoreBanking,
    BlockchainAndDigitalAssets,
    WealthTechAndRoboAdvisory,
    InsurTech,
    RiskAIAndFraudDetection,
    FintechDevOpsAndCloud
}

public static class FintechCategoryExtensions
{
    public static string ToDisplayString(this FintechCategory category) => category switch
    {
        FintechCategory.QuantAndAlgorithmicTrading => "Quant & Algorithmic Trading",
        FintechCategory.PaymentsAndOpenBanking => "Payments & Open Banking",
        FintechCategory.NeoBankingAndCoreBanking => "Neo-Banking & Core Banking",
        FintechCategory.BlockchainAndDigitalAssets => "Blockchain & Digital Assets",
        FintechCategory.WealthTechAndRoboAdvisory => "WealthTech & Robo-Advisory",
        FintechCategory.InsurTech => "InsurTech",
        FintechCategory.RiskAIAndFraudDetection => "Risk, AI & Fraud Detection",
        FintechCategory.FintechDevOpsAndCloud => "Fintech DevOps & SRE",
        _ => "All Sectors"
    };

    public static string GetIcon(this FintechCategory category) => category switch
    {
        FintechCategory.QuantAndAlgorithmicTrading => "fa-chart-line",
        FintechCategory.PaymentsAndOpenBanking => "fa-credit-card",
        FintechCategory.NeoBankingAndCoreBanking => "fa-building-columns",
        FintechCategory.BlockchainAndDigitalAssets => "fa-cubes",
        FintechCategory.WealthTechAndRoboAdvisory => "fa-sack-dollar",
        FintechCategory.InsurTech => "fa-shield-halved",
        FintechCategory.RiskAIAndFraudDetection => "fa-brain",
        FintechCategory.FintechDevOpsAndCloud => "fa-server",
        _ => "fa-globe"
    };
}
