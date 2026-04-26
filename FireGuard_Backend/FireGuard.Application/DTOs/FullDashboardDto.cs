namespace FireGuard.Application.DTOs;

public class FullDashboardDto
{
    public object Kpis { get; set; } = default!;
    public List<string> Alerts { get; set; } = new();
    public List<string> Renewals { get; set; } = new();
    public List<string> Technicians { get; set; } = new();
    public string AiSummary { get; set; } = "";
}