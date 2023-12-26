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
    /// Interaction logic for newInjury.xaml
    /// </summary>
    public partial class newInjury : Page
    {
        static int cntId = 1;

        int[] sev = {1,2,3,4,5};
        string[] injuryLoc = { 
            "Left Ankle",
            "Right Ankle",
            "Left Sholder", 
            "Right Shoulder", 
            "Left Knee", 
            "Right Knee", 
            "Lower Back",
            "Upper Back",
            "Ribs",
            "Left Wrist", 
            "Right Wrist",
            "Left Hand",
            "Right Hand",
            "Neck",
            "Pelvis",
            "Other/Unspecified"
        };
        public newInjury()
        {
            InitializeComponent();
            injuryLocationCB.ItemsSource = injuryLoc; injuryLocationCB.SelectedIndex= 0;
            severityCB.ItemsSource = sev; severityCB.SelectedIndex = 0;
            packageIDTextBox.Text = cntId.ToString();
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(packageIDTextBox.Text);
            int seve = severityCB.SelectedIndex+1;
            string loc = injuryLoc[injuryLocationCB.SelectedIndex];
            string name = packageNameTextBox.Text;
            string des = descriptionTextBox.Text;

            Injury injury=new Injury(id,name,loc,seve,des);
            DB.InsertInjury(injury);
            cntId++;
            MessageBox.Show("New injury added, ID: " + id.ToString());
            Window.GetWindow(this).Close();
        }

    }
}
