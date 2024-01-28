using clinical.BaseClasses;
using CommunityToolkit.HighPerformance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static clinical.BaseClasses.ontology;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        bool edit = false;
        Patient patient;
        List<OntologyTerm> selectedChronics = new List<OntologyTerm>();
        static List<Package> packages = DB.GetAllPackages();
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
                ageTextBox.Text = toEdit.Age.ToString();
            }
        }
        public newPatientPage()
        {
            InitializeComponent();

            assignedPhys.ItemsSource = DB.GetAllPhysiotherapists();
            ontology oi=new ontology();
            allChronicsDataGrid.ItemsSource = oi.GetFirstTenOntologies();
            allInjuriesDataGrid.ItemsSource = DB.GetAllInjuries();
            packagesCB.ItemsSource = packages;
            assignedPhys.SelectedIndex = 0;
            selectedChronicsDataGrid.ItemsSource = selectedChronics;
            selectedInjuriesDataGrid.ItemsSource = selectedInjuries;
            referredCB.Checked += CheckBox_Checked;
            referredCB.Unchecked += CheckBox_Unchecked;

            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;

            packagesCB.SelectedIndex = 0;
            selectedPackage = (Package)packagesCB.SelectedItem;

            injuryDataView = CollectionViewSource.GetDefaultView(allInjuriesDataGrid.ItemsSource);
            searchInjuriesTXTBOX.TextChanged += SearchInjuryTextBox_TextChanged;

            chronicDataView = CollectionViewSource.GetDefaultView(DB.GetAllTerms());
            searchChronicDiseaseTXTBOX.TextChanged += SearchChronicTextBox_TextChanged;


        }

        private void SearchChronicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchChronicDiseaseTXTBOX.Text.Length < 4)
            {
                return;
            }
            allChronicsDataGrid.ItemsSource = DB.GetTermsLikeName(searchChronicDiseaseTXTBOX.Text);
            //chronicDataView.Filter = item => FilterItem(item, searchChronicDiseaseTXTBOX.Text);

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

        Package selectedPackage = packages[0];

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
            int id = globals.generateNewPatientID(phone);

            List<int> injuriesIDs = new List<int>();
            foreach (Injury chronic in selectedInjuries)
            {
                injuriesIDs.Add(chronic.InjuryID);
            }

            selectedPackage = (Package)packagesCB.SelectedItem;

            double due = Double.Parse(dueTB.Text);

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
                due,
                refName,
                refPN,
                selectedPackage.PackageID,
                selectedPackage.NumberOfSessions);
            DB.InsertPatient(newPatient);


            MessageBox.Show("New patient added, ID: " + id.ToString());

            foreach (var ch in selectedChronics)
            {
                DB.InsertPatientDiseases(ch.Name, newPatient.PatientID);
            }

            foreach (var inj in selectedInjuries)
            {
                DB.InsertPatientInjuries(inj.InjuryID, newPatient.PatientID);
            }

            double paid= Double.Parse(paidTB.Text);
            Payment payment = new Payment(globals.generateNewPaymentID(id, DateTime.Now), paid, DateTime.Now, phys.UserID, id);
            DB.InsertPayment(payment);


            Window.GetWindow(this).Close();



        }





        private void addInjury(object sender, RoutedEventArgs e)
        {
            Injury selectedInjury = (Injury)allInjuriesDataGrid.SelectedItem;
            if (!selectedInjuries.Contains(selectedInjury)) selectedInjuries.Add(selectedInjury);
            refresh();
        }

        private void removeInjury(object sender, RoutedEventArgs e)
        {
            selectedInjuries.Remove((Injury)selectedInjuriesDataGrid.SelectedItem);
            refresh();
        }

        private void removeChronic(object sender, RoutedEventArgs e)
        {
            selectedChronics.Remove((OntologyTerm)selectedChronicsDataGrid.SelectedItem);
            refresh();

        }

        private void addChronic(object sender, RoutedEventArgs e)
        {
            OntologyTerm selectedChronic = (OntologyTerm)allChronicsDataGrid.SelectedItem;
            if (!selectedChronics.Contains(selectedChronic)) selectedChronics.Add(selectedChronic);
            refresh();
        }

        private void paidTC(object sender, TextChangedEventArgs e)
        {
            if (paidTB.Text != null && paidTB.Text != "")
                dueTB.Text = (selectedPackage.Price - Double.Parse(paidTB.Text.Trim())).ToString();
        }

        private void selectionPackageChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPackage = (Package)packagesCB.SelectedItem;
            if (paidTB.Text != null && paidTB.Text != "")
                dueTB.Text = (selectedPackage.Price - Double.Parse(paidTB.Text.Trim())).ToString();

        }

        
    }
}
