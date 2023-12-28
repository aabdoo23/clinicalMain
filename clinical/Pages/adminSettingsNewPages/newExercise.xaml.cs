using clinical.BaseClasses;
using System;
using System.Collections.Generic;
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

namespace clinical.Pages.adminSettingsNewPages
{
    /// <summary>
    /// Interaction logic for newExercise.xaml
    /// </summary>
    public partial class newExercise : Page
    {
        public newExercise()
        {
            InitializeComponent();
            IDTextBox.Text=new Random().Next().ToString();
        }

        bool viewing=false;
        public newExercise(Exercise exercise)
        {
            InitializeComponent();
            viewing = true;
            IDTextBox.IsEnabled = false;

            IDTextBox.Text = exercise.ExerciseID.ToString();
            nameTextBox.Text = exercise.ExerciseName;
            descriptionTextBox.Text = exercise.Notes;
            linkTextBox.Text = exercise.ExplanationLink;

        }

        public newExercise(Exercise exercise, int x)
        {
            InitializeComponent();

            IDTextBox.IsEnabled = false;

            IDTextBox.Text = exercise.ExerciseID.ToString();
            nameTextBox.Text = exercise.ExerciseName;
            descriptionTextBox.Text = exercise.Notes;
            linkTextBox.Text = exercise.ExplanationLink;

        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(IDTextBox.Text);
            string description = descriptionTextBox.Text;
            string name = nameTextBox.Text;

            string link=linkTextBox.Text;
            Exercise ex = new Exercise(id, name, link, description);

            if (viewing)
            {
                DB.UpdateExercise(ex);
                MessageBox.Show("Exercise updated, ID: " + id.ToString());
            }

            else
            {
                DB.InsertExercise(ex);
                MessageBox.Show("New Exercise added, ID: " + id.ToString());
            }
            Window.GetWindow(this).Close();
        }
    }
}
