namespace FireGuard.Domain.Entities;

public class DashboardStats
{
    public int TotalSites { get; set; }
    public decimal MonthlyRevenue { get; set; }
    public int CriticalAlerts { get; set; }
    public int RenewalsDue { get; set; }
}