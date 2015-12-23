using SQLite.Net.Async;
using System.Threading.Tasks;

namespace SQLiteModernApp.DataAccess
{
    public interface IDbConnection
    {
        Task InitializeDatabase();
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
