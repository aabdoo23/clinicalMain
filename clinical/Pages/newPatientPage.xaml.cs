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
        bool edit=false;
        Patient patient;
        public newPatientPage(Patient toEdit)
        {
            InitializeComponent();
            if (toEdit == null) edit = false;
            else { edit = true; patient = toEdit; }

            if (edit &&toEdit!=null)
            {
                firstNameTextBox.Text = toEdit.FirstName;
                lastNameTextBox.Text= toEdit.LastName;
                if(toEdit.Gender=="Male")maleRB.IsChecked=true;
                else maleRB.IsChecked=false;
                addressTextBox.Text=toEdit.Address;
                phoneTextBox.Text = toEdit.PhoneNumber;
                ageTextBox.Text = toEdit.Age().ToString();
            }
        }
        public newPatientPage()
        {
            InitializeComponent();
            assignedPhys.ItemsSource = DB.GetAllPhysiotherapists();
            assignedPhys.SelectedIndex = 0;
            
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln= lastNameTextBox.Text;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address= addressTextBox.Text;
            string phone= phoneTextBox.Text;
            DateTime bd = globals.CalculateBirthdate(int.Parse(ageTextBox.Text));
            string em = emailTextBox.Text;
            User phys = (User)(assignedPhys.SelectedItem);
            bool isRef = (bool)referredCB.IsChecked;
            bool prevSessions = (bool)prevSessionsCB.IsChecked;
            
            int id = globals.generateNewPatientID();

            Patient newPatient = new Patient(
                id,
                fn,
                ln,
                bd,
                gender,
                phone,
                em, 
                address, 
                new List<int>(),
                new List<int>(),
                phys.UserID,
                isRef,
                prevSessions,
                Convert.ToDouble(heightTextBox.Text),
                Convert.ToDouble(weightTextBox.Text),
                0,
                referringTextBox.Text,
                referringPNTextBox.Text);
            DB.InsertPatient(newPatient);
            MessageBox.Show("New patient added, ID: " +id.ToString());
            Window.GetWindow(this).Close() ;


        }

        private void addChronic(object sender, MouseButtonEventArgs e)
        {

        }

        private void viewInjury(object sender, RoutedEventArgs e)
        {

        }

        private void viewChronic(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
