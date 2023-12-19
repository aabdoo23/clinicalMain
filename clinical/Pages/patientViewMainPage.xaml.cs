using clinical.BaseClasses;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for patientViewMainPage.xaml
    /// </summary>
    public partial class patientViewMainPage : Page
    {
        private ObservableCollection<MedicalRecord> medicalRecords;
        Patient currPatient;
        public patientViewMainPage(Patient patient)
        {
            InitializeComponent();
            this.currPatient = patient;
            patientIDTxt.Text = patient.PatientID.ToString();
            patientNameMainTxt.Text = patient.FirstName + " " + patient.LastName;
            patientNameTxt.Text = patient.FirstName + " " + patient.LastName;
            ageTxt.Text = patient.Age().ToString();
            contactInfoTxt.Text = patient.PhoneNumber;
            referringTxt.Text = patient.referringName;
            noteTXT.Text = patient.Email;



            previousVisitsDataGrid.ItemsSource = DB.GetPatientVisits(patient.PatientID);

            medicalRecordsDataGrid.ItemsSource = DB.GetAllPatientRecords(patient.PatientID);
        }

        private void viewRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord record = (MedicalRecord)medicalRecordsDataGrid.SelectedItem;
            NavigationService.Navigate(new newRecordPage(record));
        }

        private void viewVisitClick(object sender, RoutedEventArgs e)
        {

        }

        private void newMedicalRecord(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new newRecordPage(currPatient));

        }

        private void enableNotes(object sender, MouseButtonEventArgs e)
        {
            noteTXT.IsEnabled = !noteTXT.IsEnabled;
        }

        private void editPersonalInfo(object sender, MouseButtonEventArgs e)
        {

        }

        private void notesUpdated(object sender, TextChangedEventArgs e)
        {
            currPatient.Email = noteTXT.Text;
        }
    }
}
