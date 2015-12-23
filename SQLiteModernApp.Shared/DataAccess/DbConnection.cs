using System.IO;
using System.Threading.Tasks;
using SQLiteModernApp.DataModel;
using SQLite.Net.Async;
using System;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace SQLiteModernApp.DataAccess
{
    public class DbConnection : IDbConnection
    {
        string dbPath;
        SQLiteAsyncConnection conn;

        public DbConnection()
        {
            dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Sample.sqlite");

            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)));
            conn = new SQLiteAsyncConnection(connectionFactory);
        }

        public async Task InitializeDatabase()
        {
            await conn.CreateTableAsync<Department>();
            await conn.CreateTableAsync<Employee>();
            await conn.CreateTableAsync<EmployeeDepartment>();
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return conn;
        }
    }
}
