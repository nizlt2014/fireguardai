using FireGuard.Application.DTOs;

namespace FireGuard.Application.Interfaces;

public interface ITechnicianRepository
{
    Task<TechniciansDto> GetTechniciansAsync();
}