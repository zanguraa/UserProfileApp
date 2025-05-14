using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _config;

    public DbConnectionFactory(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}
