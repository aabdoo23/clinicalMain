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
    /// Interaction logic for newEmployeePage.xaml
    /// </summary>
    public partial class newEmployeePage : Page
    {
        public newEmployeePage()
        {
            InitializeComponent();
            bdDatePicker.SelectedDate = DateTime.Now;
            hiringDatePicker.SelectedDate = DateTime.Now;
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
            DateTime bd = DateTime.Now;
            if (bdDatePicker.SelectedDate.HasValue)
                bd = bdDatePicker.SelectedDate.Value;

            DateTime hd = DateTime.Now;
            if (hiringDatePicker.SelectedDate.HasValue)
                hd = hiringDatePicker.SelectedDate.Value;

            string nid = NIDTextBox.Text;
            string email = emailTextBox.Text;

            User user = new User(globals.generateNewEmployeeID(nid), fn.Trim(), ln.Trim(), gender.Trim(), hd, bd, address.Trim(), phone.Trim(), email.Trim(), nid.Trim());
            DB.InsertUser(user);

            MessageBox.Show("Successfully added new Employee, ID: " + user.UserID, "Success");

            Window.GetWindow(this).Close();
        }

        private void nowCheck(object sender, RoutedEventArgs e)
        {
            hiringDatePicker.SelectedDate = DateTime.Now;
            hiringDatePicker.IsEnabled = false;
        }

        private void nowUnCheck(object sender, RoutedEventArgs e)
        {
            hiringDatePicker.IsEnabled = true;

        }

    }
}
