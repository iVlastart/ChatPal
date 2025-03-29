using ChatPal.MVVM.View.Auth;
using ChatPal.MVVM.VIewModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel main = new();
            DataContext = main;
            var loginView = new Login()
            {
                DataContext = main
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is MainViewModel main)
            {
                if(main.currentView is SigninViewModel)
                {
                    btnQuestion.Content = "Already have an account? Login";
                    btnQuestion.Command = main.LoginViewCommand;
                }
                else if(main.currentView is LoginViewModel)
                {
                    btnQuestion.Content = "Don't have an account? Signin";
                    btnQuestion.Command = main.SigninViewCommand;
                }
            }
        }
        public void setWindowForApp()
        {
            btnQuestion.Visibility = Visibility.Collapsed;
            btnConnect.Visibility = Visibility.Visible;
        }
    }
}