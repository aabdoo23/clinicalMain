using clinical.BaseClasses;
using clinical.userControls;
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
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        User loggedIn = null;
        ChatRoom selectedChatRoom=null;
        public ChatPage(User user)
        {
            InitializeComponent();
            loggedIn = user;
            List<ChatRoom> chatRooms=DB.GetChatRoomsByUserID(user.UserID);
            //<uc:chatItem Title="Dr. Mohamed Magdy"
            //Image = "/images/afsha.png"
            //                 Message = "Okay, Nice work"
            //                 Color = "#73AFFF"
            //                 MessageCount = "2" />
            foreach (ChatRoom chatRoom in chatRooms)
            {
                chatItem item = new chatItem();
                item.Title = chatRoom.ChatRoomName;
                item.MouseDown += new MouseButtonEventHandler((s, e) => chatRoom_MouseDown(s, e, chatRoom));
                chatStack.Children.Add(item);
            }

            selectedChatRoom = chatRooms[0];
            Refresh();
            textBoxMessage.Focus();
        }
        void Refresh()
        {
            roomName.Text = selectedChatRoom.ChatRoomName;
            texts.Children.Clear();
            foreach (ChatMessage ch in DB.GetAllChatMessagesByRoomID(selectedChatRoom.ChatRoomID))
            {
                //if my message -> 
                if (ch.SenderID == loggedIn.UserID)
                {
                    MyMessageChat ms = new MyMessageChat();
                    ms.Message = ch.MessageContent;
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.Hour.ToString() + ":" + ch.TimeStamp.Minute.ToString();
                    myTime.Style = FindResource("timeTextRight") as Style;

                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);

                }
                else
                {
                    MessageChat ms = new MessageChat();
                    ms.Message = ch.MessageContent;
                    ms.Color = (Brush)FindResource("darkerColor");
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.Hour.ToString() + ":" + ch.TimeStamp.Minute.ToString();
                    myTime.Style = FindResource("timeText") as Style;

                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);
                }



            }
        }
        private void chatRoom_MouseDown(object sender, MouseButtonEventArgs e,ChatRoom chatRoom)
        {
            selectedChatRoom = chatRoom;
            Refresh();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            send();
        }
        void send()
        {
            string text = textBoxMessage.Text;
            int id = globals.generateNewChatMessageID(loggedIn.UserID);
            int id2 = globals.generateNewChatMessageID(selectedChatRoom.SecondUserID);
            int chatRoomID = selectedChatRoom.ChatRoomID;
            string ch = chatRoomID.ToString();
            string ch1 = ch.ToString().Substring(0, 3);
            string ch2 = ch.ToString().Substring(3, 3);


            string ch22 = ch2 + ch1;

            int secondChatRoomID = int.Parse(ch22);
            ChatMessage message = new ChatMessage(id, loggedIn.UserID, chatRoomID, text, DateTime.Now);
            ChatMessage message2 = new ChatMessage(id2, loggedIn.UserID, secondChatRoomID, text, DateTime.Now);

            DB.InsertChatMessage(message);
            DB.InsertChatMessage(message2);
            textBoxMessage.Text = "";
            Refresh();
        }

        private void enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { send(); }
        }
    }
}
