using Models;
namespace ARClassLibrary
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T>(string storedProc, string connectionName, object parameters);
        Task SaveData(string storedProc, string connectionName, object parameters);
        Task<Users?> GetUserByUsername(string userName); // New method for retrieving user by username
    }
}