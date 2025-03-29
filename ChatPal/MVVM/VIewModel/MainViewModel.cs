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
        public ObservableCollection<UserModel> users { get; set; }
        public ObservableCollection<string> msgs { get; set; }
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
            users = new ObservableCollection<UserModel>();
            msgs = new ObservableCollection<string>();
            _server = new Server();
            _server.connectedEvent += userConnected;
            _server.msgReceivedEvent += msgReceived;
            _server.disconnectedEvent += userDisconnected;
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

            ConnectToServerCommand = new RelayCommand(obj => {
                _server.connect(Session.Session.username);
            }, o=> !string.IsNullOrEmpty(Session.Session.username));

            sendMsgCommand = new RelayCommand(obj =>
            {
                _server.sendMsgToServer(Msg);
            }, o=>!string.IsNullOrEmpty(Session.Session.username)&&!string.IsNullOrEmpty(Msg));
        }

        private void userDisconnected()
        {
            var uid = _server.packetReader.readMsg();
            var user = users.Where(x=>x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => users.Remove(user));
        }

        private void msgReceived()
        {
            var msg = _server.packetReader.readMsg();
            Application.Current.Dispatcher.Invoke(() =>
            {
                msgs.Add(msg);
            });
        }

        private void userConnected()
        {
            var user = new UserModel()
            {
                Username = _server.packetReader.readMsg(),
                UID = _server.packetReader.readMsg(),
            };

            if (!users.Any(x=>x.UID==user.UID))
            {
                Application.Current.Dispatcher.Invoke(()=>users.Add(user));
            }
        }

        public void openHome()
        {
            currentView = homeVM;
        }
    }
}