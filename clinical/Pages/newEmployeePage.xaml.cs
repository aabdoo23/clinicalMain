using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            sundayCB.IsChecked = true;
            mondayCB.IsChecked = true;
            tuesdayCB.IsChecked = true;
            wednesdayCB.IsChecked = true;
            thursdayCB.IsChecked = true;
            selectedDays.Add(2);
            selectedDays.Add(3);
            selectedDays.Add(4);
            selectedDays.Add(5);
            selectedDays.Add(6);
        }
        List<int>selectedDays=new List<int>();
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
            selectedDays=selectedDays.Distinct().ToList();
            DB.updateUserWorkDays(user.UserID, selectedDays);


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

        private void selectDay(object sender, RoutedEventArgs e)
        {
            
            if (((CheckBox)sender).Content == "Saturday")
            {
                selectedDays.Add(1);
            }
            else if(((CheckBox)sender).Content == "Sunday")
            {
                selectedDays.Add(2);
            }
            else if(((CheckBox)sender).Content == "Monday")
            {
                selectedDays.Add(3);
            }
            else if(((CheckBox)sender).Content == "Tuesday")
            {
                selectedDays.Add(4);
            }
            else if(((CheckBox)sender).Content == "Wednesday")
            {
                selectedDays.Add(5);
            }
            else if(((CheckBox)sender).Content == "Thursday")
            {
                selectedDays.Add(6);
            }
            else if (((CheckBox)sender).Content == "Friday")
            {
                selectedDays.Add(7);
            }
        }

        private void unSelectDay(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).Content == "Saturday")
            {
                selectedDays.Remove(1);
            }
            else if (((CheckBox)sender).Content == "Sunday")
            {
                selectedDays.Remove(2);
            }
            else if (((CheckBox)sender).Content == "Monday")
            {
                selectedDays.Remove(3);
            }
            else if (((CheckBox)sender).Content == "Tuesday")
            {
                selectedDays.Remove(4);
            }
            else if (((CheckBox)sender).Content == "Wednesday")
            {
                selectedDays.Remove(5);
            }
            else if (((CheckBox)sender).Content == "Thursday")
            {
                selectedDays.Remove(6);
            }
            else if (((CheckBox)sender).Content == "Friday")
            {
                selectedDays.Remove(7);
            }

        }
    }
}
