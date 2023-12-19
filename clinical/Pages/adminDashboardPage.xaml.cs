using clinical.BaseClasses;
using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminDashboardPage.xaml
    /// </summary>
    public partial class adminDashboardPage : Page
    {
        public adminDashboardPage(User admin)
        {
            InitializeComponent();
            refresh();

        }
        public void refresh()
        {
            physiciansDataGrid.ItemsSource = DB.GetAllPhysiotherapists();
            employeesDataGrid.ItemsSource= DB.GetAllEmployees();
            List<Visit> todayVisits = DB.GetTodayVisits();

            foreach (Visit v in todayVisits)
            {
                Patient patient = DB.GetPatientById(v.PatientID);
                if (patient != null)
                {
                    v.PatientName = patient.FirstName + " " + patient.LastName;
                }
                User pt = DB.GetUserById(v.PhysiotherapistID);
                if (pt != null)
                {
                    v.PhysioTherapistName = pt.FirstName + " " + pt.LastName;
                }
            }
            todayAppointmentsDataGrid.ItemsSource = todayVisits;

        }
        private void view_Click(object sender, RoutedEventArgs e)
        {

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

        private void viewPhysician(object sender, RoutedEventArgs e)
        {
            
        }

        private void startVisitClick(object sender, RoutedEventArgs e)
        {

        }

        private void deleteVisitClick(object sender, RoutedEventArgs e)
        {

        }

        private void viewVisitClick(object sender, RoutedEventArgs e)
        {
            Visit selectedVisit = (Visit)todayAppointmentsDataGrid.SelectedItem;
            visit window = new visit(selectedVisit);
            NavigationService.Navigate(window);
        }
    }
}
