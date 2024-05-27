
namespace ARClassLibrary;

public interface ISqlDataAccess
{
    Task SaveData(string storedProc, string connectionName, object parameters);
}