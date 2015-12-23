using SQLiteModernApp.DataAccess;
using SQLiteModernApp.DataModel;
using SQLiteModernApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SQLiteModernApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeePage : Page
    {
        List<Department> deptList = new List<Department>();

        DepartmentRepository oDepartmentRepository;
        EmployeeRepository oEmployeeRepository;

        Employee employee;

        public EmployeePage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitializeDatabase();
            await LoadDepartments();

            if (e.Parameter == null)
            {
                employee = new Employee();
            }
            else
            {
                employee = (Employee)e.Parameter;
            }

            sPanelEmployee.DataContext = employee;

            List<Department> listOfDepartments = (List<Department>)cboDepartment.ItemsSource;
            foreach (Department department in employee.Departments)
            {
                Department selectedDepartment = listOfDepartments.Where(d => d.DepartmentId == department.DepartmentId).FirstOrDefault();
                if (selectedDepartment != null)
                {
                    cboDepartment.SelectedItems.Add(selectedDepartment);
                }
            }
        }

        private async Task InitializeDatabase()
        {
            DbConnection oDbConnection = new DbConnection();
            await oDbConnection.InitializeDatabase();
            oDepartmentRepository = new DepartmentRepository(oDbConnection);
            oEmployeeRepository = new EmployeeRepository(oDbConnection);
        }
                
        private async Task LoadDepartments()
        {
            deptList.Add(new Department { DepartmentId = 1, DepartmentName = "Microsoft Visual Studio" });
            deptList.Add(new Department { DepartmentId = 2, DepartmentName = "Microsoft SQL Server" });
            deptList.Add(new Department { DepartmentId = 3, DepartmentName = "Microsoft Office" });
            deptList.Add(new Department { DepartmentId = 4, DepartmentName = "Microsoft Exchange Server" });

            foreach (Department item in deptList)
            {
                await oDepartmentRepository.InsertDepartmentAsync(item);
            }
            List<Department> result = await oDepartmentRepository.SelectAllDepartmentsAsync();
            cboDepartment.ItemsSource = result;
            cboDepartment.SelectedIndex = 0;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            employee.Departments = new List<Department>();
            foreach (var item in cboDepartment.SelectedItems)
            {
                employee.Departments.Add((Department)item);
            }

            if (employee.EmployeeId == 0)
            {
                await oEmployeeRepository.InsertEmployeeAsync(employee);
            }
            else
            {
                await oEmployeeRepository.UpdateEmployeeAsync(employee);
            }

            Frame.GoBack();        
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
