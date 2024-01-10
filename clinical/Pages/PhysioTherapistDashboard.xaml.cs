using clinical.BaseClasses;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for PhysioTherapistDashboard.xaml
    /// </summary>
    public partial class PhysioTherapistDashboard : Page
    {
        private DateTime currentDayIndex = DateTime.Now;
        private User physician;
        public PhysioTherapistDashboard(User therapist)
        {
            InitializeComponent();
            if (therapist == null) throw new ArgumentNullException();
            physician = therapist;
            UpdateDayBorders();
            leftSideFrame.NavigationService.Navigate(new DashBoardPage());
            signedInTB.Text = $"Welcome, Dr. {therapist.FirstName}";

        }
        void updateDayAppointments()
        {
            todayAppointmentsStackPanel.Children.Clear();
            List<Visit> visits = DB.GetPhysicianVisitsOnDate(physician.UserID,currentDayIndex);
            numberOfAppointmentsTB.Text = visits.Count.ToString();

            foreach (var i in visits)
            {
                todayAppointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            }
        }
        private void viewPatient(Patient patient)
        {

            if (patient != null)
            {
                NavigationService.Navigate(new patientViewMainPage(patient));
            }

        }
        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                NavigationService.Navigate(new visit(visit));
            }
        }
        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newAppointment(object sender, MouseButtonEventArgs e)
        {
            newAppointmentWindow window = new newAppointmentWindow();
            window.Show();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }


        void createDayUI(DateTime date)
        {

            Border border = new Border
            {
                Style = (Style)Application.Current.Resources["theBorder"],
                Background = (Brush)Application.Current.Resources["lighterColor"],
                Margin = new Thickness(5),
                Width = 55
            };
            if (date == currentDayIndex)
            {
                border.Background = (Brush)Application.Current.Resources["selectedColor"];
            }
            border.MouseDown += (sender, e) => selectDay(date);

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(24) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            TextBlock dayTextBlock = new TextBlock
            {
                Text = date.ToString("ddd"),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 16
            };

            TextBlock dateTextBlock = new TextBlock
            {
                Text = date.ToString("dd"),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 24
            };

            grid.Children.Add(dayTextBlock);
            grid.Children.Add(dateTextBlock);

            Grid.SetRow(dateTextBlock, 1);

            border.Child = grid;


            dayStack.Children.Add(border);
        }

        private void selectDay(DateTime date)
        {
            currentDayIndex = date;
            UpdateDayBorders();
        }

        private void goLeftDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(-1);
            UpdateDayBorders();
        }

        private void goRightDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(1);
            UpdateDayBorders();
        }

        private void UpdateDayBorders()
        {

            if (allPanelCB.IsChecked == true && (currentDayIndex.DayOfYear != DateTime.Now.DayOfYear))
            {
                patientsDGTitleTB.Text = currentDayIndex.ToString("M") + "' Patients";
                selectedDayTB.Text = currentDayIndex.ToString("D");
                List<Patient> patients = DB.GetPatientsWithVisitsOnDateByPhysicianID(physician.UserID,currentDayIndex);
                patientsDataGrid.ItemsSource = patients;
                numberOfPhysiciansTB.Text = patients.Count.ToString();
            }
            else
            {
                patientsDGTitleTB.Text = "Today's Patients";
                selectedDayTB.Text = currentDayIndex.ToString("M") + ", Today";
                List<Patient> patients = DB.GetPatientsWithVisitsOnDateByPhysicianID(physician.UserID, DateTime.Now);
                patientsDataGrid.ItemsSource = patients;
                numberOfPhysiciansTB.Text = patients.Count.ToString();

            }
            if (dayStack != null)
                dayStack.Children.Clear();

            //DateTime time = currentDayIndex;
            for (int i = -2; i <= 2; i++)
            {
                createDayUI(currentDayIndex.AddDays(i));
            }
            updateDayAppointments();

        }

        private void allPanelCBCheck(object sender, RoutedEventArgs e)
        {
            UpdateDayBorders();
        }



        private void dpDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDayIndex = (DateTime)dp.SelectedDate;
            UpdateDayBorders();
        }

        private void newPatient(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(0);
            window.Show();
        }

        private void viewReciptionistProfile(object sender, MouseButtonEventArgs e)
        {

        }
        
    }
}
