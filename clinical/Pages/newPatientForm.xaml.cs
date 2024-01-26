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
                window.Height = 440;    

                formTitle.Content = "New Physio Therapist";
                mainFrame.Navigate(new newEmployeePage(true));
            }
            else if (type == 2) //new employee
            {
                window.Height = 440;

                formTitle.Content = "New Employee";
                mainFrame.Navigate(new newEmployeePage(false));
            }
            else if (type == 3) //new package
            {
                window.Height = 291;
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
                window.Height = 481;
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
                window.Height = 391;
                window.Width = 500;

                formTitle.Content = "New Equipment Device";
                mainFrame.Navigate(new newEquipment());
            }
            else if(type == 8) //new chronic
            {
                window.Height = 271;
                window.Width = 400;

                formTitle.Content = "New Chronic Disease";
                mainFrame.Navigate(new newChronic());
            }
            else if (type == 9)//new exercise
            {
                window.Height = 251;
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

            else if (type == 11)
            {
                window.Height = 282;
                window.Width = 480;

                formTitle.Content = "New Patient Evaluation Test";
                mainFrame.Navigate(new newEvaluationTestPage());
            }
            else if (type == 12)
            {
                window.Height = 321;
                window.Width = 420;

                formTitle.Content = "New Appointment Type";
                mainFrame.Navigate(new newAppointmentTypePage());
            }

        }
        //view patient
        public newPatientForm(Patient editable)
        {
            InitializeComponent();
            window.Height = 621;
            formTitle.Content = "Edit Patient";
            mainFrame.Navigate(new newPatientPage(editable));
        }
        //view room
        public newPatientForm(Room toView)
        {
            InitializeComponent();
            window.Height = 481;
            window.Width = 500;
            formTitle.Content = "View Room";
            mainFrame.Navigate(new newRoom(toView));
        }

        //view equipment
        public newPatientForm(Equipment toView)
        {
            InitializeComponent();
            window.Height = 391;
            window.Width = 500;
            formTitle.Content = "View Equipment";
            mainFrame.Navigate(new newEquipment(toView));
        }

        //view exercise
        public newPatientForm(Exercise toView)
        {
            InitializeComponent();
            window.Height = 251;
            window.Width = 500;
            formTitle.Content = "View Exercise";
            mainFrame.Navigate(new newExercise(toView));
        }

        public newPatientForm(Exercise toView,int x)
        {
            InitializeComponent();
            window.Height = 251;
            window.Width = 500;
            formTitle.Content = "Add Exercise";
            mainFrame.Navigate(new newExercise(toView,1));
        }

        //view Chronic
        public newPatientForm(ChronicDisease toView)
        {
            InitializeComponent();
            window.Height = 301;
            window.Width = 400;
            formTitle.Content = "View Chronic Disease";
            mainFrame.Navigate(new newChronic(toView));
        }
        
        //view Appointment Type
        public newPatientForm(AppointmentType toView)
        {
            InitializeComponent();
            window.Height = 321;
            window.Width = 420;

            formTitle.Content = "View Appointment Type";
            mainFrame.Navigate(new newAppointmentTypePage(toView));
        }

        //view treatment plan
        public newPatientForm(TreatmentPlan toView)
        {
            InitializeComponent();
            window.Height = 321;
            window.Width = 500;
            formTitle.Content = "View Treatment Plan";
            mainFrame.Navigate(new newTreatmentPlan(toView));
        }

        //view package
        public newPatientForm(Package toView)
        {
            InitializeComponent();
            window.Height = 291;
            window.Width = 500;
            formTitle.Content = "View Package";
            mainFrame.Navigate(new newPackage(toView));
        }

        //view injury
        public newPatientForm(Injury toView)
        {
            InitializeComponent();
            window.Height = 321;
            window.Width = 500;
            formTitle.Content = "View Injury";
            mainFrame.Navigate(new newInjury(toView));
        }

        //view test
        public newPatientForm(EvaluationTest toView)
        {
            InitializeComponent();
            window.Height = 282;
            window.Width = 480;
            formTitle.Content = "View Test";
            mainFrame.Navigate(new newEvaluationTestPage(toView));
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
