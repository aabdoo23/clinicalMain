using clinical.BaseClasses;
using clinical.Pages.adminSettingsNewPages;
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
            if (type == 0) //new patient
            {
                window.Height = 721;
                formTitle.Content = "New Patient";
                mainFrame.Navigate(new newPatientPage());
            }
            else if (type == 1) //new physiotherapist
            {
                window.Height = 421;

                formTitle.Content = "New Physio Therapist";
                mainFrame.Navigate(new newPhysioTherapistPage());
            }
            else if (type == 2) //new employee
            {
                window.Height = 421;

                formTitle.Content = "New Employee";
                mainFrame.Navigate(new newEmployeePage());
            }
            else if (type == 3) //new package
            {
                window.Height = 251;
                window.Width= 500;

                formTitle.Content = "New Package";
                mainFrame.Navigate(new newPackage());
            }
            else if(type == 4) //new injury
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Injury";
                mainFrame.Navigate(new newInjury());
            }
            else if (type == 5) //new room
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Room";
                mainFrame.Navigate(new newRoom());
            }
            else if(type == 6) //new treatment plan
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Treatment Plan";
                mainFrame.Navigate(new newTreatmentPlan());
            }
            else if( type == 7) //new equipment
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Equipment Device";
                mainFrame.Navigate(new newEquipment());
            }
            else if(type == 8) //new chronic
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Chronic Disease";
                mainFrame.Navigate(new newChronic());
            }
            else if (type == 9)//new exercise
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Exercise";
                mainFrame.Navigate(new newExercise());
            }
            else if (type == 10) //new access request
            {
                window.Height = 321;
                window.Width = 500;

                formTitle.Content = "New Access Request";
                mainFrame.Navigate(new accessRequestPage());
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
