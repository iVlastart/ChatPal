using ChatPal.db;
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

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Db.addMsg(userID, txtMsg.Text);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
