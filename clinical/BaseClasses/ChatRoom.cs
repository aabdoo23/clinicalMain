using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ChatRoom
    {
        public int ChatRoomID { get; set; }
        public string FirstUserID { get; set; }
        public string SecondUserID { get; set; }
        public string ChatRoomName { get; set; }

        // Constructor
        public ChatRoom(int chatRoomID, string firstUserID, string secondUserID, string chatRoomName)
        {
            ChatRoomID = chatRoomID;
            FirstUserID = firstUserID;
            SecondUserID = secondUserID;
            ChatRoomName = chatRoomName;
        }
    }
}
