using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Room
    {
        public Room(int roomID, int capacity, string roomType, bool isAvailable)
        {
            RoomID = roomID;
            Capacity = capacity;
            RoomType = roomType;
            IsAvailable = isAvailable;
        }

        public int RoomID { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
    }
}
