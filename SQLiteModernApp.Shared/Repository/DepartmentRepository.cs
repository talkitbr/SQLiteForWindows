using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Async;
using SQLiteModernApp.DataAccess;
using SQLiteModernApp.DataModel;

namespace SQLiteModernApp.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        SQLiteAsyncConnection conn;

        public DepartmentRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task InsertDepartmentAsync(Department department)
        {
            bool isExist = await CheckDepartmentExists(department);

            if (!isExist)
            {
                await conn.InsertAsync(department);
            }
        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            await conn.DeleteAsync(department);
        }

        public async Task<List<Department>> SelectAllDepartmentsAsync()
        {
            return await conn.Table<Department>().ToListAsync();
        }

        public async Task<List<Department>> SelectDepartmentsAsync(string query)
        {
            return await conn.QueryAsync<Department>(query);
        }

        public async Task<bool> CheckDepartmentExists(Department department)
        {
            List<Department> currentDepartments = await SelectAllDepartmentsAsync();
            foreach (Department item in currentDepartments)
            {
                if (item.DepartmentId == department.DepartmentId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
