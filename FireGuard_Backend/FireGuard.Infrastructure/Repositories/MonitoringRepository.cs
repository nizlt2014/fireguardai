using Dapper;
using Microsoft.Data.SqlClient;
using FireGuard.Application.DTOs;
using FireGuard.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FireGuard.Infrastructure.Repositories;

public class MonitoringRepository : IMonitoringRepository
{
    private readonly IConfiguration _config;

    public MonitoringRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<MonitoringDto> GetMonitoringAsync()
    {
        using var con = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var devices = (await con.QueryAsync<DeviceDto>(
            @"SELECT Id, SiteName, DeviceName, DeviceType,
                     Status, LastSeen, BatteryLevel
              FROM Devices
              ORDER BY LastSeen DESC")).ToList();

        return new MonitoringDto
        {
            OnlineDevices = devices.Count,
            Warnings = devices.Count(x => x.Status == "Warning"),
            Critical = devices.Count(x => x.Status == "Critical"),
            SitesConnected = devices
                .Select(x => x.SiteName)
                .Distinct()
                .Count(),
            Devices = devices
        };
    }
}