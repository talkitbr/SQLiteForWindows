using SQLiteNetExtensions.Attributes;

namespace SQLiteModernApp.DataModel
{
    class EmployeeDepartment
    {
        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }

        [ForeignKey(typeof(Department))]
        public int DepartmentId { get; set; }
    }
}
