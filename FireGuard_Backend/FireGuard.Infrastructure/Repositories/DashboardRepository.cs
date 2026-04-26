using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FireGuard.Application.Interfaces;
using FireGuard.Domain.Entities;

namespace FireGuard.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{

    private readonly string _connectionString;

    public DashboardRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

//     public async Task<DashboardStats> GetStatsAsync()
//     {
//         var sql = @"
// SELECT
//     (SELECT COUNT(*) FROM Sites) AS TotalSites,
//     (SELECT ISNULL(SUM(MonthlyRevenue),0) FROM Sites) AS MonthlyRevenue,
//     (SELECT COUNT(*) FROM Alerts WHERE Severity = 'Critical' AND IsResolved = 0) AS CriticalAlerts,
//     (SELECT COUNT(*) FROM Renewals WHERE IsCompleted = 0) AS RenewalsDue
// ";

//         using var connection = new SqlConnection(_connectionString);

//         return await connection.QueryFirstAsync<DashboardStats>(sql);
//     }

    public async Task<int> GetTotalSitesAsync()
    {
        var sql = "SELECT COUNT(*) FROM Sites";
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<decimal> GetMonthlyRevenueAsync()
    {
        var sql = "SELECT ISNULL(SUM(MonthlyRevenue),0) FROM Sites";
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<decimal>(sql);
    }

    public async Task<int> GetCriticalAlertsAsync()
    {
        var sql = "SELECT COUNT(*) FROM Alerts WHERE Severity = 'Critical' AND IsResolved = 0";
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<int> GetRenewalsDueAsync()
    {
        var sql = "SELECT COUNT(*) FROM Renewals WHERE IsCompleted = 0";
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<List<string>> GetAlertsAsync()
    {
        var sql = "SELECT * FROM Alerts WHERE IsResolved = 0 ORDER BY CreatedDate DESC";
        using var connection = new SqlConnection(_connectionString);
        return (await connection.QueryAsync<string>(sql)).ToList();
    }

    public async Task<List<string>> GetRenewalsAsync()
    {
        var sql = "SELECT * FROM Renewals WHERE IsCompleted = 0 ORDER BY DueDate ASC";
        using var connection = new SqlConnection(_connectionString);
        return (await connection.QueryAsync<string>(sql)).ToList();
    }

    public async Task<List<string>> GetTechniciansAsync()
    {
        var sql = "SELECT * FROM Technicians";
        using var connection = new SqlConnection(_connectionString);
        return (await connection.QueryAsync<string>(sql)).ToList();
    }
    public async Task<object> GetStatsAsync()
    {
        var totalSites = await GetTotalSitesAsync();
        var revenue = await GetMonthlyRevenueAsync();
        var alerts = await GetCriticalAlertsAsync();
        var renewals = await GetRenewalsDueAsync();

        return new
        {
            totalSites,
            monthlyRevenue = revenue,
            criticalAlerts = alerts,
            renewalsDue = renewals
        };
    }
}