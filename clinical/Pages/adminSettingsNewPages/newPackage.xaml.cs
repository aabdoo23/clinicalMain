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
    /// Interaction logic for newPackage.xaml
    /// </summary>
    public partial class newPackage : Page
    {
        static int cntID = 1;
        public newPackage()
        {
            InitializeComponent();
            packageIDTextBox.Text= cntID.ToString();
        }


        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(packageIDTextBox.Text);
            double price = double.Parse(priceTextBox.Text);
            int numberOfSessions=int.Parse(numberOfSessionsTextBox.Text);
            string description = descriptionTextBox.Text;
            string packageName=packageNameTextBox.Text;

            Package package=new Package(id,packageName,numberOfSessions,price,description);
            DB.InsertPackage(package);
            cntID++;

            MessageBox.Show("New Package added, ID: " + id.ToString());
            Window.GetWindow(this).Close();
        }
    }
}
