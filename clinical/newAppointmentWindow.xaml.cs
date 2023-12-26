using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace clinical
{
    /// <summary>
    /// Interaction logic for newAppointmentWindow.xaml
    /// </summary>
    public partial class newAppointmentWindow : Window
    {
        private ICollectionView dataView;
        Patient selectedPatient = null;
        User selectedPhysician = null;
        DateTime selectedDateTime = DateTime.Now;
        DateTime lastSelectedDT = DateTime.Now;

        List<string> times = new List<string> { "16:00", "17:00", "18:00" };

        public newAppointmentWindow()
        {
            InitializeComponent();

            List<Patient> list = DB.GetAllPatients();
            foreach (Patient pat in list)
            {
                User phys = DB.GetUserById(pat.PhysicianID);
                pat.PhysicianName = phys.FirstName + " " + phys.LastName;
            }
            allPatientsDataGrid.ItemsSource = list;
            dataView = CollectionViewSource.GetDefaultView(allPatientsDataGrid.ItemsSource);
            textBoxFilter.TextChanged += textBoxFilter_TextChanged;

            datePicker.SelectedDate=DateTime.Now;

            timePicker.ItemsSource = times;
            timePicker.SelectedIndex = 0;

            physicianPicker.ItemsSource = DB.GetAllPhysiotherapists();

        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

     
        private void view(object sender, RoutedEventArgs e)
        {
            selectedPatient = (Patient)allPatientsDataGrid.SelectedItem;
            patientName.Text = selectedPatient.FirstName + " " + selectedPatient.LastName;
            physicianName.Text = selectedPatient.PhysicianName;
            selectedPhysician= DB.GetUserById(selectedPatient.PhysicianID);


        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
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

        private void saveClicked(object sender, MouseButtonEventArgs e)
        {
            if(selectedPatient == null) { }
            MessageBoxResult result = MessageBox.Show("Book "+selectedPatient.FirstName+" a reservation on "+ selectedDateTime.Month+", "
                + selectedDateTime.Day+", "+selectedDateTime.Hour+", "+selectedDateTime.Minute, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                int id = globals.generateNewVisitID(selectedPatient.PatientID, selectedDateTime);
                Visit visit = new Visit(id, selectedPhysician.UserID, selectedPatient.PatientID, 1, selectedDateTime, 1, "Follow up", "");
                DB.InsertVisit(visit);
                MessageBox.Show("Visit registered, visit id: "+id.ToString());
            }
            
        }

        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelectedDT = selectedDateTime;
            selectedDateTime = (DateTime)datePicker.SelectedDate;
        }

        private void timeChanged(object sender, SelectionChangedEventArgs e)
        {
            string s= (string)timePicker.SelectedItem;
            string hr = "",min="";
            hr+= s[0];
            hr+= s[1];
            min += s[3];
            min += s[4];

            lastSelectedDT = selectedDateTime;
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, int.Parse(hr), int.Parse(min), 0);

        }
        private void first_avail(object sender, RoutedEventArgs e)
        {
            datePicker.IsEnabled = false;
            timePicker.IsEnabled = false;

            


        }

        private void not_first_avail(object sender, RoutedEventArgs e)
        {
            selectedDateTime = lastSelectedDT;
            datePicker.IsEnabled = true;
            timePicker.IsEnabled = true;
        }

        private void physicianChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPhysician= (User)(physicianPicker.SelectedItem);
            physicianName.Text="Dr. "+selectedPhysician.FirstName+" "+selectedPhysician.LastName;
        }
    }
}
