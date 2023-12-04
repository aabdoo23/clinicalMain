using clinical.BaseClasses;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        public newPatientPage()
        {
            InitializeComponent();
            bdDatePicker.SelectedDate=DateTime.Now;
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln= lastNameTextBox.Text;
            string name = fn + " " + ln;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address= addressTextBox.Text+", "+cityTextBox.Text;
            string phone= phoneTextBox.Text;
            DateTime bd=DateTime.Now;
            if (bdDatePicker.SelectedDate.HasValue)
                bd = bdDatePicker.SelectedDate.Value;

            Random r = new Random();
            Patient newPatient = new Patient(r.Next(1, 100),name,address,bd,phone,gender);
            DB.InsertPatient(newPatient);
        }
    }
}
