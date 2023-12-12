using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            List<User> users = DB.GetAllUsers();
            
            foreach(User user1 in users)
            {
                foreach(User user2 in users)
                {
                    if (user1.UserID == user2.UserID) { continue; }
                    DB.InsertChatRoom(new ChatRoom(globals.generateNewChatRoomID(user1.UserID, user2.UserID), user1.UserID, user2.UserID, user2.FirstName));

                }
            }

            
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
