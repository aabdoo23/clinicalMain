using clinical.BaseClasses;
using clinical.Pages;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User loggedIn;
        int state;
        public MainWindow(int stat,User user)
        {
            loggedIn=user;
            state = stat;
            InitializeComponent();
            homeBTN.Focus();
            if(state==1)
                mainFrame.Navigate(new adminDashboardPage(user));
            else if(state==2)
                mainFrame.Navigate(new PhysioTherapistDashboard(user));
            else mainFrame.Navigate(new ReceptionistDashboard(user));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void homeBTN_Click(object sender, RoutedEventArgs e)
        {

            if (state == 1)
                mainFrame.Navigate(new adminDashboardPage(loggedIn));
            else if (state == 2)
                mainFrame.Navigate(new PhysioTherapistDashboard(loggedIn));
            else mainFrame.Navigate(new ReceptionistDashboard(loggedIn));

        }

        private void patientSearchBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PatientSearchPage());

        }

        private void calendarBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CalendarPage());

        }
        private void chatBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ChatPage());

        }



        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1250;
                    this.Height = 830;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }
    }
}
