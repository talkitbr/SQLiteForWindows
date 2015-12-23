using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SQLiteModernApp.DataModel
{
    [Table("Employee")]
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int EmployeeId { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [ManyToMany(typeof(EmployeeDepartment), CascadeOperations = CascadeOperation.All)]
        public List<Department> Departments { get; set; }

        public Employee()
        {
            this.Departments = new List<Department>();
        }
    }
}
