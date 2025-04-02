using ChatPal.Core;
using ChatPal.MVVM.Model;
using ChatPal.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ChatPal.MVVM.VIewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand SigninViewCommand {  get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand sendMsgCommand { get; set; }
        public LoginViewModel loginVM { get; set; }
        public SigninViewModel signinVM { get; set; }
        public HomeViewModel homeVM { get; set; }
        public string Msg { get; set; }
        private object _currentView;
        private Server _server;

        public object currentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            loginVM = new LoginViewModel();
            signinVM = new SigninViewModel();
            homeVM = new HomeViewModel();
            currentView = loginVM;
            LoginViewCommand = new RelayCommand(obj =>
            {
                currentView = loginVM;
            });
            SigninViewCommand = new RelayCommand(obj =>
            {
                currentView = signinVM;
            });
            HomeViewCommand = new RelayCommand(obj =>
            {
                currentView = homeVM;
            });
        }

        public void openHome()
        {
            currentView = homeVM;
        }
    }
}