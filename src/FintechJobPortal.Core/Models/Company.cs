namespace FintechJobPortal.Core.Models;

public class Company
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Headquarters { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string CompanySize { get; set; } = string.Empty;
    public string FundingStage { get; set; } = string.Empty; // e.g., "Public", "Series D", "Hedge Fund"
    public double GlassdoorRating { get; set; }
    public string Overview { get; set; } = string.Empty;
}
