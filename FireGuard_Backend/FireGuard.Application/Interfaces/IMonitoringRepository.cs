using FireGuard.Application.DTOs;

namespace FireGuard.Application.Interfaces;

public interface IMonitoringRepository
{
    Task<MonitoringDto> GetMonitoringAsync();
}