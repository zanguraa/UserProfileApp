using Dapper;
using Npgsql;
using UserProfile.Domain.Entities;
using UserProfile.Application.Interfaces;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace UserProfile.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _config;

    public UserRepository(IConfiguration config)
    {
        _config = config;
    }

    private IDbConnection CreateConnection()
        => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        const string sql = "SELECT COUNT(1) FROM users WHERE email = @Email";
        using var conn = CreateConnection();
        var count = await conn.ExecuteScalarAsync<int>(sql, new { Email = email });
        return count > 0;
    }

    public async Task<int> RegisterAsync(User user)
    {
        const string sql = @"
            INSERT INTO users (email, hashedpassword, role)
            VALUES (@Email, @HashedPassword, @Role)
            RETURNING id;
        ";
        using var conn = CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = "SELECT * FROM users WHERE email = @Email";
        using var conn = CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
    }
}
