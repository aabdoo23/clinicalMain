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

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminSettingsSecondPage.xaml
    /// </summary>
    public partial class adminSettingsSecondPage : Page
    {
        public adminSettingsSecondPage()
        {
            InitializeComponent();
            packagesDataGrid.ItemsSource = DB.GetAllPackages();
            roomsDataGrid.ItemsSource = DB.GetAllRooms();
            equipmentDataGrid.ItemsSource = DB.GetAllEquipment();
            //accessRequestsDataGrid.ItemsSource = DB.GetAllAccessRequests();

        }
        private void newPackage(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(3);
            form.Show();
        }
        private void newRoom(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(5);
            form.Show();
        }

        private void newEquipment(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(7);
            form.Show();
        }
        private void viewRoom(object sender, RoutedEventArgs e)
        {
            Room room = (Room)roomsDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(room);
            form.Show();
        }

        private void approveRequest(object sender, RoutedEventArgs e)
        {

        }

        private void viewRequest(object sender, RoutedEventArgs e)
        {

        }

        private void rejectRequest(object sender, RoutedEventArgs e)
        {

        }
        private void viewEquipment(object sender, RoutedEventArgs e)
        {
            Equipment eq = (Equipment)equipmentDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(eq);
            form.Show();
        }

        private void viewPackage(object sender, RoutedEventArgs e)
        {
            Package pack = (Package)packagesDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(pack);
            form.Show();
        }

        private void goToFirstPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminSettingsPage());

        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
            packagesDataGrid.ItemsSource = DB.GetAllPackages();
            roomsDataGrid.ItemsSource = DB.GetAllRooms();
            equipmentDataGrid.ItemsSource = DB.GetAllEquipment();
            //accessRequestsDataGrid.ItemsSource = DB.GetAllAccessRequests();
        }
    }
}
