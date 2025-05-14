using Dapper;
using Npgsql;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;
using System.Data;
using UserProfile.Infrastructure.Persistence.Models;

namespace UserProfile.Infrastructure.Persistence;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly IConfiguration _config;

    public UserProfileRepository(IConfiguration config)
    {
        _config = config;
    }

    private IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
    }

    public async Task<int> CreateAsync(UserProfileEntity entity)
    {
        const string sql = @"
            INSERT INTO UserProfiles (
                FullName,
                Age,
                Nationality,
                FavoriteCountries,
                VisitedCountries,
                FavoriteFootballTeam,
                Hobbies,
                Bio,
                ProfilePicture,
                CreatedAt
            )
            VALUES (
                @FullName,
                @Age,
                @Nationality,
                @FavoriteCountries,
                @VisitedCountries,
                @FavoriteFootballTeam,
                @Hobbies,
                @Bio,
                @ProfilePicture,
                @CreatedAt
            )
            RETURNING Id;
        ";

        using var conn = CreateConnection();

        var id = await conn.ExecuteScalarAsync<int>(sql, new
        {
            entity.FullName,
            entity.Age,
            entity.Nationality,
            FavoriteCountries = JsonSerializer.Serialize(entity.FavoriteCountries),
            VisitedCountries = JsonSerializer.Serialize(entity.VisitedCountries),
            entity.FavoriteFootballTeam,
            Hobbies = JsonSerializer.Serialize(entity.Hobbies),
            entity.Bio,
            entity.ProfilePicture,
            entity.CreatedAt
        });

        return id;
    }

    public async Task<List<UserProfileEntity>> GetAllAsync()
    {
        const string sql = "SELECT * FROM UserProfiles ORDER BY CreatedAt DESC";

        using var conn = CreateConnection();
        var result = await conn.QueryAsync<UserProfileRecord>(sql);

        // გადავიყვანოთ Raw → Entity (deserialize JSON)
        return result.Select(r => new UserProfileEntity
        {
            Id = r.Id,
            FullName = r.FullName,
            Age = r.Age,
            Nationality = r.Nationality,
            FavoriteCountries = JsonSerializer.Deserialize<List<string>>(r.FavoriteCountries ?? "[]"),
            VisitedCountries = JsonSerializer.Deserialize<List<string>>(r.VisitedCountries ?? "[]"),
            FavoriteFootballTeam = r.FavoriteFootballTeam,
            Hobbies = JsonSerializer.Deserialize<List<string>>(r.Hobbies ?? "[]"),
            Bio = r.Bio,
            ProfilePicture = r.ProfilePicture,
            CreatedAt = r.CreatedAt
        }).ToList();
    }

    public async Task<UserProfileEntity?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM UserProfiles WHERE Id = @Id";
        using var conn = CreateConnection();

        var raw = await conn.QuerySingleOrDefaultAsync<UserProfileRecord>(sql, new { Id = id });

        if (raw == null) return null;

        return new UserProfileEntity
        {
            Id = raw.Id,
            FullName = raw.FullName,
            Age = raw.Age,
            Nationality = raw.Nationality,
            FavoriteCountries = JsonSerializer.Deserialize<List<string>>(raw.FavoriteCountries ?? "[]"),
            VisitedCountries = JsonSerializer.Deserialize<List<string>>(raw.VisitedCountries ?? "[]"),
            FavoriteFootballTeam = raw.FavoriteFootballTeam,
            Hobbies = JsonSerializer.Deserialize<List<string>>(raw.Hobbies ?? "[]"),
            Bio = raw.Bio,
            ProfilePicture = raw.ProfilePicture,
            CreatedAt = raw.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM UserProfiles WHERE Id = @Id";
        using var conn = CreateConnection();
        var rows = await conn.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }

    public async Task<bool> UpdateAsync(UserProfileEntity entity)
    {
        const string sql = @"
        UPDATE UserProfiles SET
            FullName = @FullName,
            Age = @Age,
            Nationality = @Nationality,
            FavoriteCountries = @FavoriteCountries,
            VisitedCountries = @VisitedCountries,
            FavoriteFootballTeam = @FavoriteFootballTeam,
            Hobbies = @Hobbies,
            Bio = @Bio,
            ProfilePicture = @ProfilePicture
        WHERE Id = @Id";

        using var conn = CreateConnection();

        var rows = await conn.ExecuteAsync(sql, new
        {
            entity.FullName,
            entity.Age,
            entity.Nationality,
            FavoriteCountries = JsonSerializer.Serialize(entity.FavoriteCountries),
            VisitedCountries = JsonSerializer.Serialize(entity.VisitedCountries),
            entity.FavoriteFootballTeam,
            Hobbies = JsonSerializer.Serialize(entity.Hobbies),
            entity.Bio,
            entity.ProfilePicture,
            entity.Id
        });

        return rows > 0;
    }
}
