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
        ChatRoom selectedRoom=null;
        User loggedIn = null;
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
                item.MouseDown += chatRoom_MouseDown;
                chatStack.Children.Add(item);
            }
        }

        private void chatRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

    }
}
