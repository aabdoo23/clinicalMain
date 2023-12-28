using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for newRoom.xaml
    /// </summary>
    public partial class newRoom : Page
    {
        bool viewing = false;
        List<Equipment> selectedEquipment=new List<Equipment>();
        public newRoom()
        {
            InitializeComponent();
            IDTextBox.Text= new Random().Next(20).ToString();
            equipmentDataGrid.ItemsSource = DB.GetAllEquipment();
        }
        public newRoom(Room room)
        {
            viewing = true;
            InitializeComponent();
            IDTextBox.Text=room.RoomID.ToString();
            IDTextBox.IsEnabled = false;
            equipmentDataGrid.ItemsSource=DB.GetAllEquipment();
            selectedEquipment=DB.GetEquipmentByRoomID(room.RoomID);
            selectedEquipmentDataGrid.ItemsSource = selectedEquipment;
            NameTextBox.Text=room.RoomNumber.ToString();

        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(IDTextBox.Text);
            string name = NameTextBox.Text;

            foreach (Equipment eq in selectedEquipment)
            {
                eq.RoomID=id;
                DB.UpdateEquipment(eq);
            }

            Room room = new Room(id, name);
            if (viewing)
            {
                DB.UpdateRoom(room);
                MessageBox.Show("Room updated, ID: " + id.ToString());

            }
            else
            {
                DB.InsertRoom(room);
                MessageBox.Show("New Room added, ID: " + id.ToString());
            }
            
            Window.GetWindow(this).Close();
        }
        void Refresh()
        {
            InitializeComponent();
            equipmentDataGrid.ItemsSource = DB.GetAllEquipment();
            selectedEquipmentDataGrid.ItemsSource = selectedEquipment;
        }

        private void selectEquipment(object sender, RoutedEventArgs e)
        {
            selectedEquipment.Add((Equipment)equipmentDataGrid.SelectedItem);
            Refresh();
        }

        private void unSelectEquipment(object sender, RoutedEventArgs e)
        {
            selectedEquipment.Remove((Equipment)selectedEquipmentDataGrid.SelectedItem);
            Refresh();
        }
    }
}
