using clinical.BaseClasses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminSettingsPage.xaml
    /// </summary>
    public partial class adminSettingsPage : Page
    {
        public adminSettingsPage()
        {
            InitializeComponent();
            treatmentPlansDataGrid.ItemsSource = DB.GetAllTreatmentPlans();
            
            chronicsDataGrid.ItemsSource = DB.GetAllChronicDiseases();
            injuriesDataGrid.ItemsSource = DB.GetAllInjuries();
            exercisesDataGrid.ItemsSource = DB.GetAllExercises();
            testsDataGrid.ItemsSource=DB.GetAllTests();

        }

        private void newTreatmentPlan(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(6);
            form.Show();
        }

        

        private void newExercise(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(9);
            form.Show();
        }

        private void newChronicDisease(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(8);
            form.Show();
        }

        

        private void newInjury(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(4);
            form.Show();
        }

        private void newTest(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(11);
            form.Show();
        }

        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////


        private void viewChronic(object sender, RoutedEventArgs e)
        {
            ChronicDisease chronic = (ChronicDisease)chronicsDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(chronic);
            form.Show();
        }

        private void viewExercise(object sender, RoutedEventArgs e)
        {
            Exercise ex= (Exercise)exercisesDataGrid.SelectedItem;
            newPatientForm form=new newPatientForm(ex);
            form.Show();
        }

        

        private void viewTreatmentPlan(object sender, RoutedEventArgs e)
        {
            TreatmentPlan plan = (TreatmentPlan)treatmentPlansDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(plan);
            form.Show();
        }

        

        private void viewInjury(object sender, RoutedEventArgs e)
        {
            Injury inj = (Injury)injuriesDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(inj);
            form.Show();
        }

        private void viewTest(object sender, RoutedEventArgs e)
        {
            EvaluationTest test = (EvaluationTest)testsDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(test);
            form.Show();
        }
        private void goToSecondPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminSettingsSecondPage());

        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
            treatmentPlansDataGrid.ItemsSource = DB.GetAllTreatmentPlans();

            chronicsDataGrid.ItemsSource = DB.GetAllChronicDiseases();
            injuriesDataGrid.ItemsSource = DB.GetAllInjuries();
            exercisesDataGrid.ItemsSource = DB.GetAllExercises();
            testsDataGrid.ItemsSource = DB.GetAllTests();

        }

    }
}
