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
        public MainWindow()
        {
            InitializeComponent();
            //chatGrid.Visibility = Visibility.Hidden;
            homeBTN.Focus();
            mainFrame.Navigate(new adminDashboardPage());
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
            mainFrame.Navigate(new DashBoardPage());

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
