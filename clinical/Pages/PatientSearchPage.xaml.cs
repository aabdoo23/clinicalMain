using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PatientSearchPage.xaml
    /// </summary>
    public partial class PatientSearchPage : Page
    {
        private ObservableCollection<Patient> patients;

        public PatientSearchPage()
        {
            InitializeComponent();
            patients = new ObservableCollection<Patient>
            {
                new Patient(1, "Ali M'alol", "123 Main St", new DateTime(1980, 1, 1), "555-1234"),
                new Patient(2, "Mohamed Elshenawy", "456 Oak St", new DateTime(1990, 5, 15), "555-5678")
            };

            patientsDataGrid.ItemsSource = patients;
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
