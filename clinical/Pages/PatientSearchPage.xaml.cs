using clinical.BaseClasses;
using System.Windows;
using System.Windows.Controls;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for PatientSearchPage.xaml
    /// </summary>
    public partial class PatientSearchPage : Page
    {

        public PatientSearchPage()
        {
            InitializeComponent();

            //patientsDataGrid.ItemsSource = DB.GetAllPatients();
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)patientsDataGrid.SelectedItem;
            if (selectedPatient != null)
            {
                // Open the PatientDetailsWindow and pass the selected patient
                patientView detailsWindow = new patientView(selectedPatient);
                detailsWindow.Show();
            }
        }

        private void addNewPatient(object sender, RoutedEventArgs e)
        {
            newPatientForm form = new newPatientForm(0);
            form.Show();
        }
    }
}
