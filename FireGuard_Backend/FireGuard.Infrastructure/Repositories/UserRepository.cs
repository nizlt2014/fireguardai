using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FireGuard.Application.Interfaces;
using FireGuard.Domain.Entities;

namespace FireGuard.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var con = new SqlConnection(_connectionString);

        return await con.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email=@Email",
            new { Email = email });
    }
}