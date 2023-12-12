using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for PatientSearchPage.xaml
    /// </summary>
    public partial class PatientSearchPage : Page
    {
        private ICollectionView dataView;

        public PatientSearchPage()
        {
            InitializeComponent();
            List<Patient> list = DB.GetAllPatients();
            foreach (Patient pat in list) {
                User phys = DB.GetUserById(pat.PhysicianID);
                pat.PhysicianName = phys.FirstName+" "+phys.LastName; 
            }
            patientsDataGrid.ItemsSource = list;
            dataView = CollectionViewSource.GetDefaultView(patientsDataGrid.ItemsSource);
            textBoxFilter.TextChanged += SearchTextBox_TextChanged;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataView.Filter = item => FilterItem(item, textBoxFilter.Text);
        }
        private bool FilterItem(object item, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return true; // No filter, show all items
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var cellValue = property.GetValue(item);
                if (cellValue != null && cellValue.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Found a match in any column
                }
            }

            return false; // No match found
        }
        private void view_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)patientsDataGrid.SelectedItem;
            if (selectedPatient != null)
            {
                // Open the PatientDetailsWindow and pass the selected patient
                patientView detailsWindow = new patientView(selectedPatient);
                detailsWindow.Show();
            }
        }

        private void addNewPatient(object sender, RoutedEventArgs e)
        {
            newPatientForm form = new newPatientForm(0);
            form.Show();
        }

        private void allPatients(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //patientsDataGrid.ItemsSource = DB.GetAllPatients();

        }

        private void todaysPatients(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //patientsDataGrid.ItemsSource = DB.GetAllPatientsToday();

        }
    }
}
