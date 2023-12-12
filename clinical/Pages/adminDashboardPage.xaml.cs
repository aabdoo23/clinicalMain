﻿using clinical.BaseClasses;
using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            todayAppointmentsDataGrid.ItemsSource = DB.GetTodayVisits();
        }
        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newAppointment(object sender, MouseButtonEventArgs e)
        {
            newAppointmentWindow window = new newAppointmentWindow();
            window.Show();

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
    }
}
