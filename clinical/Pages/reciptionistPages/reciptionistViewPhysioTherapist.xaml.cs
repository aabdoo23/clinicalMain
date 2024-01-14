using clinical.BaseClasses;
using MahApps.Metro.IconPacks;
using MySqlX.XDevAPI.Relational;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for reciptionistViewPhysioTherapist.xaml
    /// </summary>
    public partial class reciptionistViewPhysioTherapist : Page
    {
        User physician;

        public reciptionistViewPhysioTherapist(User physiotherapist)
        {
            InitializeComponent();
            physician = physiotherapist;
            physicianNameMainTxt.Text = physician.FullName;
            nameTextBox.Text = physician.FullName;
            emailTextBox.Text = physician.Email;
            phoneTextBox.Text = physician.PhoneNumber;

            List<Patient> patientList = DB.GetAllPatientsByPhysicianID(physician.UserID);

            patientsDataGrid.ItemsSource = patientList;

            List<Visit> physicianUpcomingVisits = DB.GetFuturePhysicianVisits(physician.UserID);
            foreach (var i in physicianUpcomingVisits)
            {
                upcomingAppointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit, viewPatient));


            }
        }

        private void viewPatient(Patient patient)
        {

            if (patient != null)
            {
                new patientView(patient).Show();
            }

        }
        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                new patientView(visit).Show();
            }
        }

        private void viewPatientFromGrid(object sender, RoutedEventArgs e)
        {
            viewPatient((Patient)patientsDataGrid.SelectedItem);
        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}