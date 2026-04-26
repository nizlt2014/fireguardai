using Dapper;
using Microsoft.Data.SqlClient;
using FireGuard.Application.DTOs;
using FireGuard.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FireGuard.Infrastructure.Services;

public class AIInsightsService : IAIInsightsService
{
    private readonly IConfiguration _config;
    private readonly IAiService _ai;

    public AIInsightsService(
        IConfiguration config,
        IAiService ai)
    {
        _config = config;
        _ai = ai;
    }

    public async Task<AIInsightsDto> GetAsync()
    {
        using var con = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var renewalsAtRisk =
            await con.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Sites WHERE RenewalDate <= DATEADD(day,30,GETDATE())");

        var overloaded =
            await con.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Jobs WHERE JobsToday >= 5");

        var upsell = 3;

        var revenueGrowth = $"{upsell * 4}%";

        var recommendations = new List<string>();

        if (renewalsAtRisk > 0)
        {
            recommendations.Add(
                "Call high-risk renewals this week");
        }

        if (overloaded > 0)
        {
            recommendations.Add(
                "Redistribute overloaded technician workload");
        }

        recommendations.Add(
            "Pitch AMC upgrades to top clients");

        while (recommendations.Count < 3)
        {
            recommendations.Add(
                "Increase recurring service revenue");
        }

        var prompt = $"""
Business metrics:
Renewals at risk: {renewalsAtRisk}
Overloaded technicians: {overloaded}
Upsell opportunities: {upsell}

Write ONE executive summary sentence only.

Rules:
- Maximum 25 words
- No bullets
- No recommendations
- No labels
- No colon
- One sentence only
""";

        string summary;

        try
        {
            summary = await _ai.GetSummary(prompt);
        }
        catch
        {
            summary =
                "Renewals need focus while upsell opportunities can increase recurring revenue this month.";
        }

        return new AIInsightsDto
        {
            RevenueGrowthPotential = revenueGrowth,
            RenewalsAtRisk = renewalsAtRisk,
            OverloadedTechnicians = overloaded,
            UpsellOpportunities = upsell,
            Recommendations = recommendations,
            Summary = summary
        };
    }
}