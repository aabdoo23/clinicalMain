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


          ///////////////////////////////////////////////////////////////////
         ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////

        
        private void viewChronic(object sender, RoutedEventArgs e)
        {

        }

        private void viewExercise(object sender, RoutedEventArgs e)
        {

        }

        

        private void viewTreatmentPlan(object sender, RoutedEventArgs e)
        {

        }

        

        private void viewInjury(object sender, RoutedEventArgs e)
        {

        }

        private void goToSecondPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminSettingsSecondPage());

        }
    }
}
