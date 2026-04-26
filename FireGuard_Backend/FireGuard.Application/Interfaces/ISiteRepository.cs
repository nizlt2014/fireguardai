using FireGuard.Domain.Entities;

namespace FireGuard.Application.Interfaces;

public interface ISiteRepository
{
    Task<IEnumerable<Site>> GetAllAsync();
    Task<Site?> GetByIdAsync(int id);
    Task<int> CreateAsync(Site site);
    Task<bool> UpdateAsync(Site site);
    Task<bool> DeleteAsync(int id);
}