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
            var parent = (Panel)btnQuestion.Parent;
            if(parent != null) parent.Children.Remove(btnQuestion);
            StackPanel stackPanel = new()
            {
                Orientation = Orientation.Horizontal,
                Width = AppGrid.Width
            };
            
            makeRadios(stackPanel);
            AppGrid.Children.Add(stackPanel);
            Grid.SetRow(stackPanel, 1);
        }
        private void makeRadios(StackPanel stackPanel)
        {
            RadioButton radio1 = new()
            {
                Content = "Chat",
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = Brushes.White
            };
            RadioButton radio2 = new() 
            { 
                Content = "Friends",
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.White
            };
            RadioButton radio3 = new() 
            { 
                Content = "Account",
                HorizontalAlignment = HorizontalAlignment.Right,
                Foreground = Brushes.White
            };
            stackPanel.Children.Add(radio1);
            stackPanel.Children.Add(radio2);
            stackPanel.Children.Add(radio3);
        }
    }
}