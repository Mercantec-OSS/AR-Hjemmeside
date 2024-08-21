using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models; // Ensure this namespace includes the Users model

namespace ARClassLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T>(
            string storedProc,
            string connectionName,
            object parameters)
        {
            string connectionString = _config.GetConnectionString(connectionName)
                ?? throw new Exception($"Missing connection string at {connectionName}");

            using var connection = new SqlConnection(connectionString);

            var rows = await connection.QueryAsync<T>(
                storedProc,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);

            return rows.ToList();
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

        public async Task<Users?> GetUserByUsername(string userName)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Missing connection string at DefaultConnection");

            using var connection = new SqlConnection(connectionString);

            var query = "SELECT * FROM Users WHERE UserName = @UserName";
            var user = await connection.QuerySingleOrDefaultAsync<Users>(query, new { UserName = userName });

            return user;
        }
    }
}
