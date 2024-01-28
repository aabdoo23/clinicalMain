using clinical.BaseClasses;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace clinical.Pages
{

    public partial class PrintingPage : Page
    {
        public PrintingPage(TreatmentPlan plan)
        {
            InitializeComponent();
            todayDate.Text = plan.Timestamp.ToString("d");
            patientNameTextBox.Text = DB.GetPatientById(plan.PatientID).FullName;
            patientAgeTextBox.Text = DB.GetPatientById(plan.PatientID).Age.ToString();
            planTimeInWeeksTextBox.Text = plan.PlanTimeInWeeks.ToString();
            planNameTextBox.Text = plan.PlanName;
            List<IssueExercise> issueExercises = DB.GetIssuedExercisesByTreatmentPlanID(plan.PlanID);
            foreach (var i in issueExercises)
            {
                mainStackPanel.Children.Add(CreatePrescripedObject(i));
            }

        }
        public TextBlock CreatePrescripedObject(IssueExercise issue)
        {
            TextBlock prescripedTextBlock = new TextBlock
            {
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = $"-{DB.GetExerciseById(issue.ExerciseID).ExerciseName}, {issue.Frequency}, {issue.Notes}",

                Margin = new Thickness(5)
            };
            return prescripedTextBlock;
        }

        public PrintingPage(Prescription plan)
        {
            InitializeComponent();
            List<IssueScan> IssueScan = DB.GetAllIssueScansByPrescriptionID(plan.PrescriptionID);
            foreach (var i in IssueScan)
            {
                mainStackPanel.Children.Add(CreatePrescripedObject(i));
            }

        }
        public TextBlock CreatePrescripedObject(IssueScan issue)
        {
            TextBlock prescripedTextBlock = new TextBlock
            {
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = $"-{DB.GetScanTestById(issue.ScanTestID).ScanTestID}, {issue.Notes}",

                Margin = new Thickness(5)
            };
            return prescripedTextBlock;
        }



    }
}


