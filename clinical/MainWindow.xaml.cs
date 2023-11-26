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
            patientGrid.Visibility = Visibility.Hidden;
            calendarGrid.Visibility = Visibility.Hidden;
            //chatGrid.Visibility = Visibility.Hidden;
            homeBTN.Focus();

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
            mainGrid.Visibility = Visibility.Visible;
            calendarGrid.Visibility= Visibility.Hidden;
            patientGrid.Visibility = Visibility.Hidden;
            //chatGrid.Visibility = Visibility.Hidden;

        }

        private void patientSearchBTN_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            calendarGrid.Visibility= Visibility.Hidden;
            patientGrid.Visibility = Visibility.Visible;
            //chatGrid.Visibility = Visibility.Hidden;
        }

        private void calendarBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            calendarGrid.Visibility = Visibility.Visible;
            patientGrid.Visibility = Visibility.Hidden;
            //chatGrid.Visibility = Visibility.Hidden;
        }
        private void chatBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            calendarGrid.Visibility = Visibility.Hidden;
            patientGrid.Visibility = Visibility.Hidden;
            //chatGrid.Visibility=Visibility.Visible;

        }
        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void txtNote_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
        }

        private void txtTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
                lblTime.Visibility = Visibility.Collapsed;
            else
                lblTime.Visibility = Visibility.Visible;
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

        
    }
}
