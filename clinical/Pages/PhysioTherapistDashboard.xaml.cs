﻿using clinical.BaseClasses;
using System.Windows;
using System.Windows.Controls;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for PhysioTherapistDashboard.xaml
    /// </summary>
    public partial class PhysioTherapistDashboard : Page
    {
        public PhysioTherapistDashboard(User therapist)
        {

            InitializeComponent();
            patientsDataGrid.ItemsSource = DB.GetAllPatientsByPhysicianID(therapist.UserID);

        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
