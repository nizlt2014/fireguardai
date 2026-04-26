using FireGuard.Domain.Entities;

namespace FireGuard.Application.Interfaces;

public interface IDashboardRepository
{
Task<int> GetTotalSitesAsync();
Task<decimal> GetMonthlyRevenueAsync();
Task<int> GetCriticalAlertsAsync();
Task<int> GetRenewalsDueAsync();

Task<List<string>> GetAlertsAsync();
Task<List<string>> GetRenewalsAsync();
Task<List<string>> GetTechniciansAsync();
    Task<object> GetStatsAsync();
}