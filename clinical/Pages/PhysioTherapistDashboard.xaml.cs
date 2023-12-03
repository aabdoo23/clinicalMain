using System.Windows;
using System.Windows.Controls;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for PhysioTherapistDashboard.xaml
    /// </summary>
    public partial class PhysioTherapistDashboard : Page
    {
        public PhysioTherapistDashboard()
        {

            InitializeComponent();
            todayAppointmentsDataGrid.ItemsSource = globals.sampleAppointments;
            ActiveAppointmentsDataGrid.ItemsSource = globals.sampleAppointments;
            patientsDataGrid.ItemsSource = globals.samplePatients;
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
