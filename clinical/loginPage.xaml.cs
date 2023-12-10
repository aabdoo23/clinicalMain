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
using System.Windows.Shapes;

namespace clinical
{
    
    public partial class loginPage : Window
    {
        public loginPage()
        {
            new globals();
            InitializeComponent();
            new DB();
            //DB.InsertUser(new User(globals.generateNewPhysicianID("Abdelrahman", "30303030303030"), "Abdelrahman", "Saleh", "Male", DateTime.Now, DateTime.Now, "address", "01000101010", "emaily", "30303030303030"));

            //DB.InsertPatient(new Patient(globals.generateNewPatientID(), "Ahmed", "Kamal", DateTime.Now, "Male", "1010101010", "enail", "address", new List<int>(), new List<int>(), 13030, true, true, 123.3, 122.3, 123113.3));

        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
        //        textPassword.Visibility = Visibility.Collapsed;
        //    else
        //        textPassword.Visibility = Visibility.Visible;
        //}

        //private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    passwordBox.Focus();
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(passwordBox.Password))
        //    {
        //        MessageBox.Show("Successfully Signed In");
        //    }
        //}

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmailHint.Visibility = Visibility.Collapsed;
            else
                textEmailHint.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void logInBTN_Click(object sender, RoutedEventArgs e)
        {

            User user=DB.GetUserById(Convert.ToInt32(txtEmail.Text));
            if (user != null)
            {
                //MessageBox.Show("Successfully Signed In"+user.HireDate.ToString());
                int s;
                if (txtEmail.Text.ToString().StartsWith("1")) s = 1;
                else if (txtEmail.Text.ToString().StartsWith("2")) s = 2;
                else s = 3;
                MainWindow mainWindow = new MainWindow(s,user);
                mainWindow.Show();
                this.Close();


            }
            else if (user==null) { 
                MessageBox.Show("Invalid ID");
            }
            

        }
    }

}
