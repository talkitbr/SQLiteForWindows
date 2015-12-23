using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteModernApp.DataModel;

namespace SQLiteModernApp.Repository
{
    interface IEmployeeRepository
    {
        Task InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        Task<List<Employee>> SelectAllEmployeesAsync();
        Task<List<Employee>> SelectEmployeesAsync(string query);
    }
}
