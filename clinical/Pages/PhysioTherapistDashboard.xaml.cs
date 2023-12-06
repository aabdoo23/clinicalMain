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
            //patientsDataGrid.ItemsSource = DB.GetAllPatients();

        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
