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
    /// Interaction logic for patientViewMainPage.xaml
    /// </summary>
    public partial class patientViewMainPage : Page
    {
        private ObservableCollection<MedicalRecord> medicalRecords;

        public patientViewMainPage(Patient patient)
        {
            InitializeComponent();
            patientIDTxt.Text = patient.PatientID.ToString();
            patientNameMainTxt.Text = patient.Name;
            patientNameTxt.Text = patient.Name;
            ageTxt.Text = patient.Age.ToString();
            contactInfoTxt.Text = patient.PhoneNumber;

            medicalRecords = new ObservableCollection<MedicalRecord>
            {
                new MedicalRecord(1,patient.PatientID,new DateTime(2023,2,23),"good","Lab Results"),
                new MedicalRecord(2,patient.PatientID,new DateTime(2023,2,23),"good","Screen Results")
            };

            medicalRecordsDataGrid.ItemsSource = medicalRecords;
        }

        private void viewRecord_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new recordPage());
        }

        private void viewVisitClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
