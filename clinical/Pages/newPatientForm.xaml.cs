using clinical.BaseClasses;
using System.Windows;
using System.Windows.Input;

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
            else if (type == 1)
            {
                window.Height = 421;

                formTitle.Content = "New Physio Therapist";
                mainFrame.Navigate(new newPhysioTherapistPage());
            }
            else if (type == 2)
            {
                window.Height = 421;

                formTitle.Content = "New Employee";
                mainFrame.Navigate(new newEmployeePage());
            }

        }

        public newPatientForm(Patient editable)
        {
            InitializeComponent();
            window.Height = 621;
            formTitle.Content = "Edit Patient";
            mainFrame.Navigate(new newPatientPage(editable));
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
                Window.GetWindow(this).DragMove();
            }
        }
    }
}
