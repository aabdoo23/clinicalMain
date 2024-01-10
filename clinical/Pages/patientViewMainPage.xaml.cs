using clinical.BaseClasses;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PdfSharp.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for patientViewMainPage.xaml
    /// </summary>
    public partial class patientViewMainPage : Page
    {
        private ObservableCollection<MedicalRecord> medicalRecords;
        Patient currPatient;
        public patientViewMainPage(Patient patient)
        {
            InitializeComponent();
            if (NavigationService!=null&&NavigationService.CanGoBack == false)
            {
                goBackIcon.Visibility = Visibility.Hidden;

            }
            this.currPatient = patient;
            patientIDTxt.Text = patient.PatientID.ToString();
            patientNameMainTxt.Text = patient.FirstName + " " + patient.LastName;
            patientNameTxt.Text = patient.FirstName + " " + patient.LastName;
            ageTxt.Text = patient.Age.ToString();
            contactInfoTxt.Text = patient.PhoneNumber;
            referringTxt.Text = patient.referringName;
            noteTXT.Text = patient.Email;

            LoadChart();

            List<ChronicDisease> patientChronics = DB.GetAllChronicDiseasesByPatientID(currPatient.PatientID);
            foreach(ChronicDisease ch in patientChronics)
            {
                CreateChronicBorder(ch);
            }

            List<Injury> patientInjuries =DB.GetAllInjuriesByPatientID(currPatient.PatientID);
            foreach(Injury inj in patientInjuries)
            {
                CreateInjuryBorder(inj);
            }

            previousVisitsDataGrid.ItemsSource = DB.GetPatientPrevVisits(patient.PatientID);
            currVisitsDataGrid.ItemsSource = DB.GetPatientTodayVisits(patient.PatientID);

            medicalRecordsDataGrid.ItemsSource = DB.GetAllPatientRecords(patient.PatientID);


        }

        private void viewRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord record = (MedicalRecord)medicalRecordsDataGrid.SelectedItem;
            NavigationService.Navigate(new newRecordPage(record));
        }

        private void viewVisitClick(object sender, RoutedEventArgs e)
        {
            Visit vis = (Visit)previousVisitsDataGrid.SelectedItem;
            if(vis==null)vis= (Visit)currVisitsDataGrid.SelectedItem;
            NavigationService.Navigate(new visit(vis));

        }

        private void newMedicalRecord(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new newRecordPage(currPatient));

        }

        private void enableNotes(object sender, MouseButtonEventArgs e)
        {
            noteTXT.IsEnabled = !noteTXT.IsEnabled;
        }

        private void editPersonalInfo(object sender, MouseButtonEventArgs e)
        {

        }



        //private void LoadChart()
        //{

        //        List<EvaluationTestFeedBack> feedBacks = DB.GetFeedbackByPatient(currPatient.PatientID);
        //        SeriesCollection = new LiveCharts.SeriesCollection();

        //        var distinctTestIds = feedBacks.Select(feedback => feedback.TestID).Distinct();

        //        foreach (var testId in distinctTestIds)
        //        {
        //            var feedbacksForTestId = feedBacks.Where(feedback => feedback.TestID == testId);

        //            // Group feedbacks by month
        //            var groupedByMonth = feedbacksForTestId
        //                .GroupBy(feedback => feedback.TimeStamp.Month)
        //                .OrderBy(group => group.Key);

        //            LineSeries lineSeries = new LineSeries
        //            {
        //                Title = DB.GetTestById(testId).TestName,
        //                Values = new ChartValues<int>(groupedByMonth.SelectMany(group => group.Select(feedback => feedback.Severity))),
        //                //PointGeometry = null // This removes the point marker
        //            };

        //            SeriesCollection.Add(lineSeries);
        //        }

        //        // Set the X-axis labels dynamically based on the months present in the data and sort them
        //        Labels = feedBacks.Select(feedback => feedback.TimeStamp.ToString("MMM")).Distinct().OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month).ToArray();
        //        YFormatter = value => value;

        //        DataContext = this;

        //}

        private bool displayMonths = true; // Initial display is months

        private void LoadChart()
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            List<EvaluationTestFeedBack> feedBacks = DB.GetFeedbackByPatient(currPatient.PatientID);
            SeriesCollection = new LiveCharts.SeriesCollection();

            var distinctTestIds = feedBacks.Select(feedback => feedback.TestID).Distinct();

            foreach (var testId in distinctTestIds)
            {
                var feedbacksForTestId = feedBacks.Where(feedback => feedback.TestID == testId);

                IEnumerable<IGrouping<int, EvaluationTestFeedBack>> groupedData;

                if (displayMonths)
                {
                    // Group feedbacks by month
                    groupedData = feedbacksForTestId
                        .GroupBy(feedback => feedback.TimeStamp.Month)
                        .OrderBy(group => group.Key);
                }
                else
                {
                    // Group feedbacks by week
                    groupedData = feedbacksForTestId
                        .GroupBy(feedback => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(feedback.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                        .OrderBy(group => group.Key);
                }

                LineSeries lineSeries = new LineSeries
                {
                    Title = DB.GetTestById(testId).TestName,
                    Values = new ChartValues<int>(groupedData.SelectMany(group => group.Select(feedback => feedback.Severity))),
                    PointGeometry = null // This removes the point marker
                };

                SeriesCollection.Add(lineSeries);
            }

            // Set the X-axis labels dynamically based on the selected time unit
            Labels = displayMonths
                ? feedBacks.Select(feedback => feedback.TimeStamp.ToString("MMM")).Distinct().OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month).ToArray()
                : feedBacks.Select(feedback => $"Week {CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(feedback.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}").Distinct().OrderBy(week => int.Parse(week.Substring(5))).ToArray();

            YFormatter = value => value;

            DataContext = this;
        }

        
        private void monthlyWeekly(object sender, MouseButtonEventArgs e)
        {
            displayMonths = !displayMonths;
            UpdateChart();
            if(displayMonths)monthlyWeeklyTB.Text = "Monthly";
            else monthlyWeeklyTB.Text = "Weekly";
            
        }

        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, double> YFormatter { get; set; }


        public void CreateChronicBorder(ChronicDisease chronic)
        {
            Border border = new Border
            {
                Background = new SolidColorBrush(Colors.FloralWhite),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 10, 0, 0)
            };

            TextBlock textBlock = new TextBlock
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Text = chronic.ChronicDiseaseName,
                Style = (Style)Application.Current.Resources["titleText"],
                TextWrapping = TextWrapping.Wrap
            };

            double maxBorderWidth = chronicWrapPanel.ActualWidth; 
            
            double desiredWidth = MeasureTextWidth(chronic.ChronicDiseaseName, textBlock.FontSize) + 100; 

            border.MinWidth = Math.Min(desiredWidth, maxBorderWidth);

            border.Child = textBlock;

            border.MinWidth += 60;
            border.MinHeight += 30;
            chronicWrapPanel.Children.Add(border);
        }


        public void CreateInjuryBorder(Injury inj)
        {
            Border border = new Border
            {
                Background = new SolidColorBrush(Colors.FloralWhite),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 10, 0, 0)
            };

            TextBlock textBlock = new TextBlock
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Text = inj.InjuryName,
                Style = (Style)Application.Current.Resources["titleText"],
                TextWrapping = TextWrapping.Wrap
            };

            double maxBorderWidth = injuriesWrapPanel.ActualWidth; 

            double desiredWidth = MeasureTextWidth(inj.InjuryName, textBlock.FontSize);

            border.MinWidth = Math.Min(desiredWidth, maxBorderWidth);

            border.MinWidth += 60;
            border.MinHeight += 30;

            border.Child = textBlock;

            injuriesWrapPanel.Children.Add(border);
        }

        private double MeasureTextWidth(string text, double fontSize)
        {
            FormattedText formattedText = new(
                text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"), 
                fontSize,
                Brushes.Black 
            );

            return formattedText.Width;
        }


        private void notesUpdated(object sender, TextChangedEventArgs e)
        {
            currPatient.Email = noteTXT.Text;
        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
