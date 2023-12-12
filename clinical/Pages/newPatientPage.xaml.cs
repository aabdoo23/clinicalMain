using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        bool edit = false;
        Patient patient;
        List<ChronicDisease> selectedChronics = new List<ChronicDisease>();
        List<Injury> selectedInjuries = new List<Injury>();
        public newPatientPage(Patient toEdit)
        {
            InitializeComponent();
            if (toEdit == null) edit = false;
            else { edit = true; patient = toEdit; }

            if (edit && toEdit != null)
            {
                firstNameTextBox.Text = toEdit.FirstName;
                lastNameTextBox.Text = toEdit.LastName;
                if (toEdit.Gender == "Male") maleRB.IsChecked = true;
                else maleRB.IsChecked = false;
                addressTextBox.Text = toEdit.Address;
                phoneTextBox.Text = toEdit.PhoneNumber;
                ageTextBox.Text = toEdit.Age().ToString();
            }
        }
        public newPatientPage()
        {
            InitializeComponent();
            assignedPhys.ItemsSource = DB.GetAllPhysiotherapists();
            allChronicsDataGrid.ItemsSource = DB.GetAllChronicDiseases();
            allInjuriesDataGrid.ItemsSource = DB.GetAllInjuries();
            assignedPhys.SelectedIndex = 0;
            referredCB.Checked += CheckBox_Checked;
            referredCB.Unchecked += CheckBox_Unchecked;


        }

        void refresh()
        {
            selectedChronicsDataGrid.ItemsSource = selectedChronics;
            selectedInjuriesDataGrid.ItemsSource=selectedInjuries;
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            referringTextBox.IsEnabled = true;
            referringPNTextBox.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;
        }
        private void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln = lastNameTextBox.Text;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address = addressTextBox.Text;
            string phone = phoneTextBox.Text;
            DateTime bd = globals.CalculateBirthdate(int.Parse(ageTextBox.Text));
            string em = emailTextBox.Text;
            User phys = (User)(assignedPhys.SelectedItem);
            bool isRef = (bool)referredCB.IsChecked;
            bool prevSessions = (bool)prevSessionsCB.IsChecked;
            string refName="",refPN="";
            if (isRef)
            {
                refName = referringTextBox.Text;
                refPN = referringPNTextBox.Text;

            }
            int id = globals.generateNewPatientID();
            List<int>chronicsIDs=new List<int>();   
            foreach(ChronicDisease chronic in selectedChronics)
            {
                chronicsIDs.Add(chronic.ChronicDiseaseID);
            }
            List<int>injuriesIDs=new List<int>();   
            foreach(Injury chronic in selectedInjuries)
            {
                injuriesIDs.Add(chronic.InjuryID);
            }
            Patient newPatient = new Patient(
                id,
                fn,
                ln,
                bd,
                gender,
                phone,
                em,
                address,
                chronicsIDs,
                injuriesIDs,
                phys.UserID,
                isRef,
                prevSessions,
                Convert.ToDouble(heightTextBox.Text),
                Convert.ToDouble(weightTextBox.Text),
                0,
                refName,
                refPN);
            DB.InsertPatient(newPatient);
            MessageBox.Show("New patient added, ID: " + id.ToString());
            Window.GetWindow(this).Close();


        }

      
        
        

        private void addInjury(object sender, RoutedEventArgs e)
        {
            selectedInjuries.Add((Injury)allInjuriesDataGrid.SelectedItem);
            refresh();
        }

        private void removeInjury(object sender, RoutedEventArgs e)
        {
            selectedInjuries.Remove((Injury)selectedInjuriesDataGrid.SelectedItem);
            refresh();
        }

        private void removeChronic(object sender, RoutedEventArgs e)
        {
            selectedChronics.Remove((ChronicDisease)selectedChronicsDataGrid.SelectedItem);
            refresh();

        }

        private void addChronic(object sender, RoutedEventArgs e)
        {
            selectedChronics.Add((ChronicDisease)allChronicsDataGrid.SelectedItem);
            refresh();
        }
    }
}
