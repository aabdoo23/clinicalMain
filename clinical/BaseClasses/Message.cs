using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Message
    {
        public Message(int messageID, int senderID, int recipientID, string content, DateTime timeStamp)
        {
            MessageID = messageID;
            SenderID = senderID;
            RecipientID = recipientID;
            Content = content;
            TimeStamp = timeStamp;
        }

        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
