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
    /// Interaction logic for newChronic.xaml
    /// </summary>
    public partial class newChronic : Page
    {
        public newChronic()
        {
            InitializeComponent();

            chronicIDTextBox.Text = new Random().Next().ToString();
        }
        bool viewing=false;

        public newChronic(ChronicDisease chronic)
        {
            InitializeComponent();
            viewing=true;
            chronicIDTextBox.IsEnabled = false;

            chronicIDTextBox.Text = chronic.ChronicDiseaseID.ToString();
            descriptionTextBox.Text=chronic.Description;
            chronicNameTextBox.Text=chronic.ChronicDiseaseName;
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(chronicIDTextBox.Text);
            string description = descriptionTextBox.Text;
            string name = chronicNameTextBox.Text;

            ChronicDisease ch=new ChronicDisease(id, name,description);

            if (viewing)
            {
                DB.updateChronicDisease(ch);

                MessageBox.Show("Chronic Disease updated, ID: " + id.ToString());
            }
            else
            {
                DB.InsertChronicDisease(ch);

                MessageBox.Show("New Chronic Disease added, ID: " + id.ToString());
            }
            
            Window.GetWindow(this).Close();
        }
    }
}
