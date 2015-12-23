using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SQLiteModernApp.DataAccess;
using SQLiteModernApp.DataModel;
using SQLiteModernApp.Repository;
using System.Linq;
using SQLiteModernApp;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SQLiteModernApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Department> deptList = new List<Department>();

        EmployeeRepository oEmployeeRepository;

        Employee employee;

        public MainPage()
        {
            this.InitializeComponent();        
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitializeDatabase();
            await UpdateListView();
        }

        private async Task InitializeDatabase()
        {
            DbConnection oDbConnection = new DbConnection();
            await oDbConnection.InitializeDatabase();
            oEmployeeRepository = new EmployeeRepository(oDbConnection);
        }

        private async Task UpdateListView()
        {
            Employee oEmployee = new Employee();
            List<Employee> result = await oEmployeeRepository.SelectAllEmployeesAsync();
            
            lstViewEmployees.ItemsSource = result;

            AppBarButton_Delete.IsEnabled = false;
            AppBarButton_Edit.IsEnabled = false;
        }
        

        private void lstViewEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            employee = e.AddedItems[0] as Employee;

            AppBarButton_Delete.IsEnabled = true;
            AppBarButton_Edit.IsEnabled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeePage));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeePage), employee);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await oEmployeeRepository.DeleteEmployeeAsync(employee);
            await UpdateListView();
        }
    }
}
