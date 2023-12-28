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
        public newPackage()
        {
            InitializeComponent();
            packageIDTextBox.Text= new Random().Next().ToString();
        }

        bool viewing = false;
        public newPackage(Package pack)
        {
            InitializeComponent();
            viewing = true;
            packageIDTextBox.Text = pack.PackageID.ToString();
            packageIDTextBox.IsEnabled = false;
            priceTextBox.Text = pack.Price.ToString();
            numberOfSessionsTextBox.Text= pack.NumberOfSessions.ToString();
            descriptionTextBox.Text=pack.Description.ToString();
            packageNameTextBox.Text= pack.PackageName.ToString();

        }


        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(packageIDTextBox.Text);
            double price = double.Parse(priceTextBox.Text);
            int numberOfSessions=int.Parse(numberOfSessionsTextBox.Text);
            string description = descriptionTextBox.Text;
            string packageName=packageNameTextBox.Text;

            Package package=new Package(id,packageName,numberOfSessions,price,description);


            if (viewing)
            {
                DB.UpdatePackage(package);
                MessageBox.Show("Package updated, ID: " + id.ToString());

            }
            else
            {
                DB.InsertPackage(package);
                MessageBox.Show("New Package added, ID: " + id.ToString());
            }
            
            Window.GetWindow(this).Close();
        }
    }
}
