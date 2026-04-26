using Microsoft.AspNetCore.Mvc;
using FireGuard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FireGuard.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardRepository _repo;

    public DashboardController(IDashboardRepository repo)
    {
        _repo = repo;
        var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
Console.WriteLine(hash);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _repo.GetStatsAsync();
        return Ok(data);
    }
    [HttpGet("full")]
public async Task<IActionResult> Full(
    [FromServices] IDashboardRepository repo,
    [FromServices] AiService ai)
{
    var totalSites = await repo.GetTotalSitesAsync();
    var revenue = await repo.GetMonthlyRevenueAsync();
    var renewalsDue = await repo.GetRenewalsDueAsync();

    var alerts = new List<string>
    {
        "Smoke spike - ABC Mall",
        "Hydrant pressure low - Nova Tower"
    };

    var technicians = new List<string>
    {
        "Ravi Kumar - ETA 28 mins",
        "Arjun Patel - ETA 41 mins"
    };

    var prompt = $@"
Sites: {totalSites}
Revenue: {revenue}
Renewals Due: {renewalsDue}
Alerts: {string.Join(",", alerts)}

Give 3 short business recommendations.";

    var aiSummary = await ai.GetSummary(prompt);

    var result = new
    {
        kpis = new
        {
            totalSites,
            monthlyRevenue = revenue,
            criticalAlerts = alerts.Count,
            renewalsDue
        },
        alerts,
        renewals = new[]
        {
            "ABC Mall - 5 days",
            "Sunrise Mall - 9 days"
        },
        technicians,
        aiSummary
    };

    return Ok(result);
}
}