using FireGuard.Application.DTOs;

namespace FireGuard.Application.Interfaces;

public interface IAIInsightsService
{
    Task<AIInsightsDto> GetAsync();
}