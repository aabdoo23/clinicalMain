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
    /// Interaction logic for viewPhysiotherapist.xaml
    /// </summary>
    public partial class viewPhysiotherapist : Page
    {
        public viewPhysiotherapist(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                firstNameTextBox.Text = user.FirstName;
                lastNameTextBox.Text = user.LastName;   
                addressTextBox.Text= user.Address;  
                genderTextBox.Text= user.Gender;
                emailTextBox.Text= user.Email;
                NIDTextBox.Text = user.NationalID;
                phoneTextBox.Text = user.PhoneNumber;
            }
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
