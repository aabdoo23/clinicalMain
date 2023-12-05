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
    /// Interaction logic for newPatientForm.xaml
    /// </summary>
    public partial class newPatientForm : Window
    {
        public newPatientForm(int type)
        {
            InitializeComponent();
            if (type == 0)
            {
                window.Height = 621;
                formTitle.Content = "New Patient";
                mainFrame.Navigate(new newPatientPage());
            }
            else if(type == 1) {
                window.Height = 421;

                formTitle.Content = "New Physio Therapist";
                mainFrame.Navigate(new newPhysioTherapistPage());
            }
            else if(type==2)
            {
                window.Height = 421;

                formTitle.Content = "New Employee";
                mainFrame.Navigate(new newEmployeePage());
            }
        }

        private void closeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }
        private void minimizeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
