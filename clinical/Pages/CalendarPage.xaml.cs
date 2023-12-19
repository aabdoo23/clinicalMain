using clinical.BaseClasses;
using clinical.userControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        string[] months = {"January", "February", "March", "April", "May", "June", "July",
            "August", "September", "October", "November", "December" };

        private Button selectedYrButton=null;
        private Button selectedMnthButton=null;


        int leftestYr =DateTime.Now.Year-2, rightestYr= DateTime.Now.Year + 2;

        DateTime selectedDay;
        public CalendarPage()
        {
            InitializeComponent();

            selectedDay=DateTime.Now;
            Refresh();
            
            refreshYears();

            for(int i = 1; i <= 12; i++)
            {
                int m=i;
                Button mnthBtn = new Button
                {
                    Content = m.ToString(),
                    Style = (Style)FindResource("calendarButtonMonth")
                };
                mnthBtn.Click += (sender, e) => switchToMnth(sender as Button, m);

                if (m == DateTime.Now.Month)
                {
                    selectedMnthButton = mnthBtn;
                    mnthBtn.Style = (Style)FindResource("calendarButtonMonthBig");
                }

                mnthStack.Children.Add(mnthBtn);
            }            

        }

        void refreshYears()
        {
            yrStack.Children.Clear();
            for (int i = leftestYr; i <= rightestYr; i++)
            {
                
                int yr = i;
                Button yrBtn = new Button
                {
                    Content = i.ToString(),
                    Style = (Style)FindResource("calendarButton")
                };

                yrBtn.Click += (sender, e) => switchToYr(sender as Button, yr);

                if (yr == DateTime.Now.Year) {
                    selectedYrButton = yrBtn;
                    yrBtn.Style = (Style)FindResource("calendarButtonBig");
                }
                yrStack.Children.Add(yrBtn);
            }
        }
        void Refresh()
        {
            string lab= months[selectedDay.Month - 1] + " "+ selectedDay.Year.ToString();
            monthMainLBL.Text = lab;

            dayTB.Text = selectedDay.Day.ToString();

            monthTB.Text = months[selectedDay.Month - 1];
            dayOfTheWeekTB.Text = selectedDay.DayOfWeek.ToString();
            calendar.DisplayDate = selectedDay;
            
            
            DateTime dateTime = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day);
            List<Visit> todayVisits = DB.GetPhysicianVisitsOnDate(globals.signedIn.UserID, dateTime);
            
            todayTaskCnt.Text = todayVisits.Count.ToString()+" tasks- n dates left";

           
            visitsStackPanel.Children.Clear();
            foreach (Visit visit1 in todayVisits)
            {
                Item itemControl = new Item();
                itemControl.Title = visit1.VisitID.ToString();
                itemControl.Time = visit1.TimeStamp.Hour.ToString()+" : "+ visit1.TimeStamp.Minute.ToString();
                itemControl.Color = (SolidColorBrush)FindResource("lightFontColor");
                visitsStackPanel.Children.Add(itemControl);
            }
            

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

        private void selectedDayChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedDay= calendar.SelectedDate ?? DateTime.MinValue;
            Refresh();
        }
        
        void switchToYr(Button sender, int year)
        {
            if (selectedYrButton != null)
            {
                selectedYrButton.Style = (Style)FindResource("calendarButton");
            }

            sender.Style = (Style)FindResource("calendarButtonBig");
            selectedYrButton = sender;
            DateTime now= new DateTime(year,selectedDay.Month,selectedDay.Day);
            selectedDay=now;

            Refresh();
        }

        void switchToMnth(Button sender, int month)
        {
            if (selectedMnthButton != null)
            {
                selectedMnthButton.Style = (Style)FindResource("calendarButtonMonth");
            }

            sender.Style = (Style)FindResource("calendarButtonMonthBig");
            selectedMnthButton = sender;
            DateTime now = new DateTime(selectedDay.Year, month, selectedDay.Day);
            selectedDay = now;

            Refresh();
        }

        void goLeftYr(object sender, RoutedEventArgs e)
        {
            leftestYr--;
            rightestYr--;
            refreshYears();
        }

        private void prevDay(object sender, RoutedEventArgs e)
        {
            DateTime now = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day - 1);
            selectedDay = now;

            Refresh();
        }

        private void nextDay(object sender, RoutedEventArgs e)
        {
            DateTime now = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day + 1);
            selectedDay = now;

            Refresh();
        }

        private void goRightYr(object sender, RoutedEventArgs e)
        {
            leftestYr++;
            rightestYr++;
            refreshYears();
        }

    }
}
