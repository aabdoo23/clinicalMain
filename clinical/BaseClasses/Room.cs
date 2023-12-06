using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public List<int> EquipmentIDs { get; set; }

        // Constructor
        public Room(int roomID, string roomNumber, List<int> equipmentIDs)
        {
            RoomID = roomID;
            RoomNumber = roomNumber;
            EquipmentIDs = equipmentIDs;
        }
    }
}
