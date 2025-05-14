using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(); // System.Data
}