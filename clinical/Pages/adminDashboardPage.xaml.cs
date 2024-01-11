using clinical.BaseClasses;
using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;
using LiveCharts.Wpf;
using LiveCharts;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

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
            UpdateFinancesChart();
            List<Visit> visits = DB.GetAllVisitsOnDate(DateTime.Now);
            foreach (var i in visits)
            {
                appointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            }


        }

        void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                NavigationService.Navigate(new visit(visit));
            }
        }
        void viewPatient(Patient patient)
        {
            if (patient != null)
            {
                NavigationService.Navigate(new patientViewMainPage(patient));
            }
        }
        private void UpdateFinancesChart()
        {
            List<Payment> payments = DB.GetAllPayments();

            SeriesCollection s = new LiveCharts.SeriesCollection();
            financesChart.Series = s; // Set the series collection for the specific chart

            var distinctPhysicianIds = payments.Select(payment => payment.PhysicianID).Distinct();
            List<Brush> barColors = new List<Brush> { (Brush)Application.Current.Resources["lightFontColor"], (Brush)Application.Current.Resources["lighterColor"], (Brush)Application.Current.Resources["selectedColor"], Brushes.AntiqueWhite};
            int colorInd=0;

            foreach (var physicianId in distinctPhysicianIds)
            {
                var paymentsForPhysician = payments.Where(payment => payment.PhysicianID == physicianId);

                ColumnSeries columnSeries = new ColumnSeries
                {
                    Title = $"Dr. {DB.GetUserById(physicianId).FullName}",
                    Values = new ChartValues<double> { paymentsForPhysician.Sum(payment => payment.Amount) },
                    LabelPoint = point => point.Y.ToString("C"),
                    Fill = barColors[colorInd] // Customize the color if needed
                };
                colorInd = (colorInd + 1) % barColors.Count;
                s.Add(columnSeries);
            }

            // Set the X-axis labels dynamically based on physician IDs
            financesChart.AxisX[0].Labels = distinctPhysicianIds.Select(id => $"Dr. {DB.GetUserById(id).FullName}").ToArray();
            financesChart.AxisY[0].LabelFormatter = value => value.ToString("C"); // Use currency format if applicable


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
            new viewUser((User)physiciansDataGrid.SelectedItem).Show();
        }

        private void startVisitClick(object sender, RoutedEventArgs e)
        {

        }

        private void deleteVisitClick(object sender, RoutedEventArgs e)
        {

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
