using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminDashboardPage.xaml
    /// </summary>
    public partial class adminDashboardPage : Page
    {
        public adminDashboardPage()
        {
            InitializeComponent();
            employeesDataGrid.ItemsSource=DB.GetAllEmployees();
            physiciansDataGrid.ItemsSource = DB.GetAllPhysioTherapists();
            employeesDataGrid.ItemsSource = DB.GetAllEmployees();
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newAppointment(object sender, MouseButtonEventArgs e)
        {
            newAppointmentWindow window = new newAppointmentWindow();
            window.Show();
        }

        private void newEmployee(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(2);
            window.Show();
        }

        private void newPhysician(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(1);
            window.Show();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
