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
    /// Interaction logic for ReceptionistDashboard.xaml
    /// </summary>
    public partial class ReceptionistDashboard : Page
    {
        public ReceptionistDashboard()
        {
            InitializeComponent();
            //patientsDataGrid.ItemsSource = DB.GetAllPatients();

        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newEmployee(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(0);
            window.Show();
        }

        private void newAppointment(object sender, MouseButtonEventArgs e)
        {

        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
