using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SQLiteModernApp.DataModel
{
    [Table("Department")]
    public class Department
    {
        [PrimaryKey]
        public int DepartmentId { get; set; }

        [MaxLength(30)]
        public string DepartmentName { get; set; }        
    }
}
