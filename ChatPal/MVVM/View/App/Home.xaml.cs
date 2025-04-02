using ChatPal.db;
using ChatPal.MVVM.View.Msg;
using ChatPal.MVVM.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
using WebSocketSharp;

namespace ChatPal.MVVM.View.App
{
    /// <summary>
    /// Interakční logika pro Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            //send icon uni => U+F6C0 and for xaml &#xF6C0;
            InitializeComponent();
        }
        string userID = Db.getID(Session.Session.username);
        public MainViewModel main => Application.Current.MainWindow.DataContext as MainViewModel;

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Db.addMsg(userID, txtMsg.Text);
            loadMsgs(Session.Session.username, txtMsg.Text);
            txtMsg.Text = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string[][] datas = Db.getMsgs();
            string username = string.Empty;
            string content = string.Empty;
            foreach (var data in datas)
            {
                //MessageBox.Show($"UserID: {row[0]}\nMessage: {row[1]}", "Message Info");
                username = Db.getUsername(data[0]);
                content = data[1];
                loadMsgs(username, content);
            }
        }

        private void loadMsgs(string username, string content)
        {
            var msg = new ChatPal.MVVM.View.Msg.Msg
            {
                Username = username,
                Content = content
            };
            Grid.SetRowSpan(msg, 4);
            Grid.SetColumnSpan(msg, 4);
            msgStack.Children.Add(msg);
        }
    }
}
