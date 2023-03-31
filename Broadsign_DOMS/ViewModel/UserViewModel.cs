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
        private Domains _domain;
        private ObservableCollection<UserModel> _userList;
        private ObservableCollection<GroupModel> _groupList;
        private ObservableCollection<ContainerScopeRelationModel> _containerScopeRelations;
        private ObservableCollection<ContainerScopeModel> _containerScopes;
        private ObservableCollection<ContainerModel> _containers;
        private UserModel _selectedModelUser;
        private string _userName;
        private string _fullName;
        private int _domain_Id;
        private int _container_Id;
        private CloneUserModel _cloneUserModel = new CloneUserModel();
        private ICommand _pushUser;
        #endregion
        #region Properties
        public Domains Domain
        {

            get => _domain;
            set
            {
                _domain = value;
                OnPropertyChanged(nameof(_domain));
                _generateList();

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
                _searchRelation();
            }
        }

        public CloneUserModel CloneUserModel
        {
            get
            {
                return _cloneUserModel;
            }
            set
            {
                _cloneUserModel = value;
                OnPropertyChanged(nameof(CloneUserModel));
            }
        }

        public ObservableCollection<ContainerScopeRelationModel> ContainerScopeRelations
        {
            get
            {
                if (_containerScopeRelations == null)
                    _containerScopeRelations = new ObservableCollection<ContainerScopeRelationModel>();
                return _containerScopeRelations;
            }
            set
            {
                _containerScopeRelations = value;
                OnPropertyChanged(nameof(ContainerScopeRelations));
            }
        }
        public ObservableCollection<ContainerScopeModel> ContainerScopes
        {
            get
            {
                if (_containerScopes == null)
                    _containerScopes = new ObservableCollection<ContainerScopeModel>();
                return _containerScopes;
            }
            set
            {
                _containerScopes = value;
                OnPropertyChanged(nameof(ContainerScopes));
            }

        }

        public ObservableCollection<ContainerModel> Containers
        {
            get
            {
                return _containers ?? new ObservableCollection<ContainerModel>();
            }
            set
            {
                _containers = value;
                OnPropertyChanged(nameof(ContainerModel));
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

        public ObservableCollection<GroupModel> GroupList 
        {
            get
            {
                if(_groupList == null)
                    _groupList = new ObservableCollection<GroupModel>();
                return _groupList;
            }
            set
            {
                _groupList = value;
                OnPropertyChanged(nameof(GroupList));
            } 
        }
        #endregion
        #region Constructors
        public UserViewModel()
        {
            
            Messenger.Default.Register<Domains>(this, "UserViewModel", message => Domain = message);


        }
        #endregion
        #region Methods
        private void _searchRelation()
        {
            ////TODO Convert foreach into linqreturns 0
            ///
            if(SelectedModelUser != null)
            {
                List<ContainerScopeRelationModel> groupids = CommonResources.Container_Scope_Relations.Where(x => x.User_id == SelectedModelUser.Id).ToList();
                List<ContainerScopeModel> scopeids = CommonResources.Container_Scopes.Where(x => x.Parent_id == SelectedModelUser.Id).ToList();


                CloneUserModel.Group_ids = groupids;
                CloneUserModel.ScopingRelation_ids = scopeids;
                CloneUserModel.Id = SelectedModelUser.Id;
                CloneUserModel.Name = SelectedModelUser.Name;
                CloneUserModel.Username = SelectedModelUser.Username;
                CloneUserModel.UserContainer_id = SelectedModelUser.Container_id;
                CloneUserModel.Groups = new List<GroupModel>();
                foreach (var groupid in groupids)
                {
                    CloneUserModel.Groups.Add(CommonResources.Groups.Where(x => x.Id == groupid.Parent_id).First());
                }
                    


                Messenger.Default.Send(CloneUserModel, "AdminViewModel");
            }
         
        }

        private void _generateList()
        {
          
                
            //var users = CommonResources.User;
            if (Domain != null)
            {
                //assign abstract user model list to users and assing it to the local userlist 
                var userList = CommonResources.Users.Where(d => d.Domain_name == Domain.Domain).ToList();
                var groupList = CommonResources.Groups.Where(d => d.Domain_name == Domain.Domain).ToList();
                UserList = new ObservableCollection<UserModel>(userList);
                GroupList = new ObservableCollection<GroupModel>(groupList);
            }
            else
            {
                UserList = CommonResources.Users;
                GroupList = CommonResources.Groups;
            }


        }

        private void pushUserApi()
        {
            UserModel modeluser = new UserModel { Name = FullName, Username = UserName, Domain_id = this.DomainId, Container_id = this.ContainerId };
            UserModel.AddUsers(Domain.Token, modeluser);

        }
        #endregion
    }
}
