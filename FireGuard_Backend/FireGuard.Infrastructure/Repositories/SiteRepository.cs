using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FireGuard.Application.Interfaces;
using FireGuard.Domain.Entities;

namespace FireGuard.Infrastructure.Repositories;

public class SiteRepository : ISiteRepository
{
    private readonly string _connectionString;

    public SiteRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<Site>> GetAllAsync()
    {
        using var con = new SqlConnection(_connectionString);
        return await con.QueryAsync<Site>("SELECT * FROM Sites ORDER BY Id DESC");
    }

    public async Task<Site?> GetByIdAsync(int id)
    {
        using var con = new SqlConnection(_connectionString);
        return await con.QueryFirstOrDefaultAsync<Site>(
            "SELECT * FROM Sites WHERE Id=@Id",
            new { Id = id });
    }

    public async Task<int> CreateAsync(Site site)
    {
        var sql = @"
INSERT INTO Sites(Name, City, Status, MonthlyRevenue)
VALUES(@Name,@City,@Status,@MonthlyRevenue);
SELECT CAST(SCOPE_IDENTITY() as int);";

        using var con = new SqlConnection(_connectionString);

        return await con.ExecuteScalarAsync<int>(sql, site);
    }

    public async Task<bool> UpdateAsync(Site site)
    {
        var sql = @"
UPDATE Sites
SET Name=@Name,
    City=@City,
    Status=@Status,
    MonthlyRevenue=@MonthlyRevenue
WHERE Id=@Id";

        using var con = new SqlConnection(_connectionString);

        var rows = await con.ExecuteAsync(sql, site);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var con = new SqlConnection(_connectionString);

        var rows = await con.ExecuteAsync(
            "DELETE FROM Sites WHERE Id=@Id",
            new { Id = id });

        return rows > 0;
    }
}