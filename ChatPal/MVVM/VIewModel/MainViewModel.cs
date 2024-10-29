using ChatPal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatPal.MVVM.VIewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand SigninViewCommand {  get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public LoginViewModel loginVM { get; set; }
        public SigninViewModel signinVM { get; set; }
        public HomeViewModel homeVM { get; set; }
        private object _currentView;

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