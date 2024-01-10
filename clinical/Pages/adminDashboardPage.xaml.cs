using clinical.BaseClasses;
using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;

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
            hereNowDataGrid.ItemsSource=DB.GetAllUserswRecordsByDate(DateTime.Now);
            List<Visit> todayVisits = DB.GetTodayVisits();

            
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

        private void deleteUser(object sender, RoutedEventArgs e)
        {
            User selectedUser=(User)employeesDataGrid.SelectedItem;
            if (selectedUser == null)
            {
                selectedUser = (User)physiciansDataGrid.SelectedItem;
            }
            MessageBoxResult result=MessageBox.Show($"Are you sure you want to delete {selectedUser.FirstName} {selectedUser.LastName} ?","Delete User",MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                DB.DeleteUser(selectedUser.UserID);
                MessageBox.Show($"{selectedUser.FirstName} data has been deleted successfully.");
            }

        }
    }
}
