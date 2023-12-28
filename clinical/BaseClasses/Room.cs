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

        public Room(int roomID, string roomNumber)
        {
            RoomID = roomID;
            RoomNumber = roomNumber;
        }
        override public string ToString()
        {
            return "ID: " + RoomID.ToString() + ", " + RoomNumber;
        }
    }
}
