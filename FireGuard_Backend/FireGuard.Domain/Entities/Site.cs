namespace FireGuard.Domain.Entities;

public class Site
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string City { get; set; } = "";
    public string Status { get; set; } = "";
    public decimal MonthlyRevenue { get; set; }
}