using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ARClassLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task SaveData(
        string storedProc,
        string connectionName,
        object parameters)
    {
        string connectionString = _config.GetConnectionString(connectionName)
            ?? throw new Exception($"Missing connection string at {connectionName}");

        using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            storedProc,
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
