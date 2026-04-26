namespace FireGuard.Application.DTOs;

public class AIInsightsDto
{
    public string RevenueGrowthPotential { get; set; } = "";
    public int RenewalsAtRisk { get; set; }
    public int OverloadedTechnicians { get; set; }
    public int UpsellOpportunities { get; set; }

    public List<string> Recommendations { get; set; } = new();

    public string Summary { get; set; } = "";
}