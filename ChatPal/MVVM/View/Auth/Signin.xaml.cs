using ChatPal.db;
using ChatPal.MVVM.VIewModel;
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

namespace ChatPal.MVVM.View.Auth
{
    /// <summary>
    /// Interakční logika pro Signin.xaml
    /// </summary>
    public partial class Signin : UserControl
    {
        public MainViewModel main => Application.Current.MainWindow.DataContext as MainViewModel;
        public Signin()
        {
            InitializeComponent();
        }

        private void signin(object sender, RoutedEventArgs e)
        {
            if(Db.checkErrors(txtUsername.Text, pswPassword.Password) != "") lblError.Content = Db.checkErrors(txtUsername.Text, pswPassword.Password);
            else
            {
                Session.Session.username = txtUsername.Text;
                Session.Session.password = pswPassword.Password;
                Db.addUser(txtUsername.Text, pswPassword.Password);
                Session.Session.ID = Db.getID(txtUsername.Text);
                if(Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.Dispatcher.Invoke(()=>mainWin.setWindowForApp());
                }
                main?.openHome();
            }
        }
    }
}
