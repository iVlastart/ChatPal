using ChatPal.db;
using ChatPal.MVVM.VIewModel;
using ChatPal.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interakční logika pro Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public MainViewModel main => Application.Current.MainWindow.DataContext as MainViewModel;
        public Login()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            if(Db.checkErrors(txtUsername.Text, pswPassword.Password) == "")
            {
                bool login = Db.logUser(txtUsername.Text, pswPassword.Password);
                if (login)
                {
                    Session.Session.ID = Db.getID(txtUsername.Text);
                    Session.Session.username = txtUsername.Text;
                    if (Application.Current.MainWindow is MainWindow mainWin)
                    {
                        mainWin.Dispatcher.Invoke(() => mainWin.setWindowForApp());
                    }
                    main?.openHome();
                }
            }
            else lblError.Content = Db.checkErrors(txtUsername.Text, pswPassword.Password);
        }
    }
}