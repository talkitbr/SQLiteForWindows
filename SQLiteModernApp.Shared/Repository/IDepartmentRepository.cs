using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteModernApp.DataModel;

namespace SQLiteModernApp.Repository
{
    interface IDepartmentRepository
    {
        Task InsertDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
        Task<List<Department>> SelectAllDepartmentsAsync();
        Task<List<Department>> SelectDepartmentsAsync(string query);
        Task<bool> CheckDepartmentExists(Department department);
    }
}
