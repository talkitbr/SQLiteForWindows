using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Async;
using SQLiteModernApp.DataAccess;
using SQLiteModernApp.DataModel;
using SQLiteNetExtensionsAsync.Extensions;

namespace SQLiteModernApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        SQLiteAsyncConnection conn;

        public EmployeeRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await conn.InsertOrReplaceWithChildrenAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await conn.UpdateWithChildrenAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await conn.DeleteAsync(employee);
        }

        public async Task<List<Employee>> SelectAllEmployeesAsync()
        {
            return await conn.GetAllWithChildrenAsync<Employee>();
        }

        public async Task<List<Employee>> SelectEmployeesAsync(string query)
        {
            return await conn.QueryAsync<Employee>(query);
        }
    }
}
