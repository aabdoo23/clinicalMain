using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private ICollectionView injuryDataView;
        private ICollectionView chronicDataView;


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
            packagesCB.ItemsSource = DB.GetAllPackages();
            assignedPhys.SelectedIndex = 0;
            
            referredCB.Checked += CheckBox_Checked;
            referredCB.Unchecked += CheckBox_Unchecked;

            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;

            packagesCB.SelectedIndex = 0;

            injuryDataView = CollectionViewSource.GetDefaultView(allInjuriesDataGrid.ItemsSource);
            searchInjuriesTXTBOX.TextChanged += SearchInjuryTextBox_TextChanged;


            chronicDataView = CollectionViewSource.GetDefaultView(allChronicsDataGrid.ItemsSource);
            searchChronicDiseaseTXTBOX.TextChanged += SearchChronicTextBox_TextChanged;


        }

        private void SearchChronicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            chronicDataView.Filter = item => FilterItem(item, searchChronicDiseaseTXTBOX.Text);

        }

        private void SearchInjuryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryDataView.Filter = item => FilterItem(item, searchInjuriesTXTBOX.Text);

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
        void refresh()
        {
            selectedChronicsDataGrid.Items.Refresh();
            selectedInjuriesDataGrid.Items.Refresh();
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
            string refName = "", refPN = "";
            if (isRef)
            {
                refName = referringTextBox.Text;
                refPN = referringPNTextBox.Text;

            }
            int id = globals.generateNewPatientID();
            
            List<int> injuriesIDs = new List<int>();
            foreach (Injury chronic in selectedInjuries)
            {
                injuriesIDs.Add(chronic.InjuryID);
            }

            Package selectedPackage = (Package)packagesCB.SelectedItem;

            Patient newPatient = new(
                id,
                fn,
                ln,
                bd,
                gender,
                phone,
                em,
                address,
                phys.UserID,
                isRef,
                prevSessions,
                Convert.ToDouble(heightTextBox.Text),
                Convert.ToDouble(weightTextBox.Text),
                0,
                refName,
                refPN,
                selectedPackage.PackageID);
            DB.InsertPatient(newPatient);
            MessageBox.Show("New patient added, ID: " + id.ToString());
            
            foreach(var ch in selectedChronics)
            {
                DB.InsertPatientChronicDiseases(ch.ChronicDiseaseID, newPatient.PatientID);
            }

            foreach(var inj in selectedInjuries)
            {
                DB.InsertPatientInjuries(inj.InjuryID, newPatient.PatientID);
            }

            Window.GetWindow(this).Close();



        }





        private void addInjury(object sender, RoutedEventArgs e)
        {
            Injury selectedInjury= (Injury)allInjuriesDataGrid.SelectedItem;
            if (!selectedInjuries.Contains(selectedInjury))selectedInjuries.Add(selectedInjury);
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
            ChronicDisease selectedChronic = (ChronicDisease)allChronicsDataGrid.SelectedItem;
            if (!selectedChronics.Contains(selectedChronic))selectedChronics.Add(selectedChronic);
            refresh();
        }
    }
}
