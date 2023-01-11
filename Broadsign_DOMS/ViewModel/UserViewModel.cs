using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Broadsign_DOMS.ViewModel
{
    public class UserViewModel : ObservableObject, IPageViewModel
    {
        private Domains domain;
        private ObservableCollection<UserModel> userList;
        public UserViewModel()
        {
            Messenger.Default.Register<Domains>(this, message => Domain = message);
        }

        public Domains Domain 
        { 

            get => domain;
            set
            {
                domain = value;
                OnPropertyChanged(nameof(domain));
                _generateList();
            }
        }

        public ObservableCollection<UserModel> UserList 
        {
            get
            {
                if (userList == null)
                    userList = new ObservableCollection<UserModel>();
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }

        private void _generateList()
        {
            dynamic users = UserModel.getUser(Domain.Token);
            if(users != null)
            {
                foreach(var user in users["user"])
                {

                }
            }
        }
    }
}
