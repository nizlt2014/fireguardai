using FireGuard.Domain.Entities;

namespace FireGuard.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}