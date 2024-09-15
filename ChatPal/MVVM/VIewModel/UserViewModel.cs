using ChatPal.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPal.MVVM.VIewModel
{
    internal class UserViewModel : INotifyPropertyChanged
    {
		private User _user;

		public  User User
		{
			get { return _user; }
			set
			{
				_user = value;
				OnPropertyChanged("Person");
			}
		}

        public UserViewModel(string name, string username, string email, byte[] password)
        {
			User = new User
			{
				Name = name,
				Username = username,
				Email = email,
				Password = password
			};
        }

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

    }
}
