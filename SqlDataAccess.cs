public async Task<int> SaveData(string storedProcedure, string connectionStringName, Users user)
{
    using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionStringName)))
    {
        var parameters = new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            user.Password,
            user.Role,
            user.Picture,
            user.FileName,
            user.UserName
        };

        return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
