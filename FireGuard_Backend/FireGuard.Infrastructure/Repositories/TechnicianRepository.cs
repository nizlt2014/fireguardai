using Dapper;
using Microsoft.Data.SqlClient;
using FireGuard.Application.DTOs;
using FireGuard.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FireGuard.Infrastructure.Repositories;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly IConfiguration _config;

    public TechnicianRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<TechniciansDto> GetTechniciansAsync()
    {
        using var con = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var sql = @"
        SELECT t.Id,
               t.Name,
               t.Status,
               t.Skill,
               ISNULL(j.SiteName,'-') SiteName,
               ISNULL(j.ETAMinutes,0) ETAMinutes,
               ISNULL(j.JobsToday,0) JobsToday
        FROM Technicians t
        LEFT JOIN Jobs j ON t.Id = j.TechnicianId";

        var rows = (await con.QueryAsync<TechnicianItemDto>(sql)).ToList();
        //Console.WriteLine("Total rows = " + rows.Count);
        return new TechniciansDto
        {
            TotalTechnicians = rows.Count,
            ActiveJobs = rows.Count(x => x.Status != "Available"),
            AvailableNow = rows.Count(x => x.Status == "Available"),
            AvgEta = (int)Math.Round(
            rows
            .Where(x => x.ETAMinutes > 0)
            .DefaultIfEmpty()
            .Average(x => x?.ETAMinutes ?? 0)
            ),
            Technicians = rows
        };
    }
}