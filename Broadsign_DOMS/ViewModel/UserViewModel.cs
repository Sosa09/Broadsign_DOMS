using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class UserViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private string _domain;
        private ObservableCollection<UserModel> _userList;
        private ObservableCollection<GroupModel> _groupList;
        private UserModel _selectedModelUser;


        private string _userName;
        private string _fullName;
        private string _search;
        private int _domain_Id;
        private int _container_Id;

        private ICommand _pushUser;

        #endregion
        #region Properties
        public string Domain
        {

            get => _domain;
            set
            {
                _domain = value;
                OnPropertyChanged("Domain");
                _updateUserList();

            }
        }
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged("Search");
                _updateUserList();
            }
        }

        public ObservableCollection<UserModel> UserList
        {
            get
            {
                if (_userList == null)
                    _userList = new ObservableCollection<UserModel>();
                return _userList;
            }
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }


        public UserModel SelectedModelUser
        {
            get => _selectedModelUser;
            set
            {
                _selectedModelUser = value;
                OnPropertyChanged(nameof(SelectedModelUser));
            }
        }



        public ICommand PushUser
        {
            get
            {

                if (_pushUser == null)
                {
                    _pushUser = new RelayCommand(x => pushUserApi());
                }
                return _pushUser;
            }
        }

        public string UserName 
        { 
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(UserName);
            } 
        }
        public string FullName 
        { 
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(FullName);
            }
        }
        public int DomainId 
        { 
            get => _domain_Id;
            set
            {
                _domain_Id = value;
                OnPropertyChanged(nameof(DomainId));
            }
        }
        public int ContainerId 
        { 
            get => _container_Id;
            set
            {
                _container_Id = value;
                OnPropertyChanged(nameof(ContainerId));
                    
            }
        }

        #endregion
        #region Constructors
        public UserViewModel()
        {
            
            Messenger.Default.Register<string>(this, "DomainUserViewModel", message => Domain = message);
            Messenger.Default.Register<string>(this, "SearchUserViewModel", message => Search= message);


        }
        #endregion
        #region Methods
        private void _updateUserList()
        {
            if (Domain == null && (Search == null || Search == string.Empty))
                UserList = new ObservableCollection<UserModel>(CommonResources.Users);
            else if(Domain != null && (Search == null || Search == string.Empty))
                UserList = new ObservableCollection<UserModel>(CommonResources.Users.Where(x => x.Domain.Name == _domain));
            else if(Domain != null && (Search != null || Search != string.Empty))
                UserList = new ObservableCollection<UserModel>(CommonResources.Users.Where(x => x.Name.ToLower().Contains(_search.ToLower())));
            else
                UserList = new ObservableCollection<UserModel>(CommonResources.Users.Where(x => x.Name.ToLower().Contains(_search.ToLower()) && x.Domain.Name == Domain));


        }

        private void pushUserApi()
        {
            UserModel modeluser = new UserModel { Name = FullName, Username = UserName, Domain_id = this.DomainId, Container_id = this.ContainerId };
            UserModel.AddUsers(modeluser.Domain, modeluser);

        }
        #endregion
    }
}
