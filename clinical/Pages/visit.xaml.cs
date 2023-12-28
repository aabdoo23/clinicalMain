using clinical.BaseClasses;
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
    /// Interaction logic for visit.xaml
    /// </summary>
    public partial class visit : Page
    {
        Visit currVisit;
        Patient patient;
        public visit(Visit selectedVisit )
        {
            InitializeComponent();
            currVisit=selectedVisit;
            patient = DB.GetPatientById(selectedVisit.PatientID);

            patientNameMainTxt.Text = patient.FirstName+" "+patient.LastName;
            patientNameText.Text = patient.FirstName + " " + patient.LastName;
            visitIdText.Text = currVisit.VisitID.ToString();
            visitDate.Text=currVisit.TimeStamp.DayOfWeek.ToString();
            physicianName.Text=patient.PhysicianName;

            noteTXT.Text=currVisit.TherapistNotes;
            prescriptionsDataGrid.ItemsSource = DB.GetAllPrescriptionsByVisitID(currVisit.VisitID);
        }

        private void viewPrescription(object sender, RoutedEventArgs e)
        {
            Prescription prescription = (Prescription)prescriptionsDataGrid.SelectedItem;
            new prescriptionWindow(prescription).Show();
        }

        private void removeLastTest(object sender, MouseButtonEventArgs e)
        {

        }

        private void addTest(object sender, MouseButtonEventArgs e)
        {

        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void newPrescription(object sender, MouseButtonEventArgs e)
        {
            prescriptionWindow window=new prescriptionWindow(currVisit,patient);
            window.Show();
        }

        private void notes_enable(object sender, MouseButtonEventArgs e)
        {
            noteTXT.IsEnabled = !noteTXT.IsEnabled;
        }
    }
}
