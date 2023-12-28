using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
    /// Interaction logic for newEquipment.xaml
    /// </summary>
    public partial class newEquipment : Page
    {
        List<Room> allRooms=DB.GetAllRooms();
        public newEquipment()
        {
            InitializeComponent();
            roomCB.ItemsSource = allRooms;
            roomCB.SelectedIndex = 0;
            IDTextBox.Text=new Random().Next(50).ToString();
        }

        bool viewing = false;

        public newEquipment(Equipment equipment)
        {
            InitializeComponent();
            viewing = true;
            IDTextBox.IsEnabled = false;

            roomCB.ItemsSource = allRooms;
            IDTextBox.Text = equipment.EquipmentID.ToString();
            NameTextBox.Text = equipment.EquipmentName;
            descriptionTextBox.Text = equipment.Function;
            int cnt = 0;
            foreach (Room r in allRooms)
            {
                if (r.RoomID == equipment.RoomID)
                {
                    roomCB.SelectedIndex = cnt;
                    break;
                }
                cnt++;
            }
            maintenanceDateTP.SelectedDate = equipment.LatestMaintenanceDate;
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(IDTextBox.Text);
            string description = descriptionTextBox.Text;
            string name = NameTextBox.Text;


            int roomId = ((Room)roomCB.SelectedItem).RoomID;

            DateTime dt = (DateTime)maintenanceDateTP.SelectedDate;

            Equipment eq = new Equipment(id, name, description, dt, false,roomId);

            if (viewing)
            {
                DB.UpdateEquipment(eq);
                MessageBox.Show("Equipment Updated, ID: " + id.ToString());
            }

            else{
                DB.InsertEquipment(eq);
                MessageBox.Show("New Exercise added, ID: " + id.ToString());
            }
            
            Window.GetWindow(this).Close();
        }
    }
}
