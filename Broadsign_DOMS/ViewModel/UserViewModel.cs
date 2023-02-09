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
        private Domains domain;
        private ObservableCollection<UserModel> userList;
        private ObservableCollection<GroupModel> groupList;
        private ObservableCollection<ContainerScopeRelationModel> containerScopeRelations;
        private ObservableCollection<ContainerScopeModel> containerScopes;
        private ObservableCollection<ContainerModel> containers;
        private UserModel selectedModelUser;
        private string userName;
        private string fullName;
        private int domain_Id;
        private int container_Id;
        private CloneUserModel cloneUserModel = new CloneUserModel();
        private ICommand pushUser;
        #endregion
        #region Properties
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

        public UserModel SelectedModelUser
        {
            get => selectedModelUser;
            set
            {
                selectedModelUser = value;
                OnPropertyChanged(nameof(SelectedModelUser));
                _searchRelation();
            }
        }

        public CloneUserModel CloneUserModel
        {
            get
            {
                return cloneUserModel;
            }
            set
            {
                cloneUserModel = value;
                OnPropertyChanged(nameof(CloneUserModel));
            }
        }

        public ObservableCollection<ContainerScopeRelationModel> ContainerScopeRelations
        {
            get
            {
                if (containerScopeRelations == null)
                    containerScopeRelations = new ObservableCollection<ContainerScopeRelationModel>();
                return containerScopeRelations;
            }
            set
            {
                containerScopeRelations = value;
                OnPropertyChanged(nameof(ContainerScopeRelations));
            }
        }
        public ObservableCollection<ContainerScopeModel> ContainerScopes
        {
            get
            {
                if (containerScopes == null)
                    containerScopes = new ObservableCollection<ContainerScopeModel>();
                return containerScopes;
            }
            set
            {
                containerScopes = value;
                OnPropertyChanged(nameof(ContainerScopes));
            }

        }

        public ObservableCollection<ContainerModel> Containers
        {
            get
            {
                return containers ?? new ObservableCollection<ContainerModel>();
            }
            set
            {
                containers = value;
                OnPropertyChanged(nameof(ContainerModel));
            }
        }

        public ICommand PushUser
        {
            get
            {

                if (pushUser == null)
                {
                    pushUser = new RelayCommand(x => pushUserApi());
                }
                return pushUser;
            }
        }

        public string UserName 
        { 
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(UserName);
            } 
        }
        public string FullName 
        { 
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged(FullName);
            }
        }
        public int DomainId 
        { 
            get => domain_Id;
            set
            {
                domain_Id = value;
                OnPropertyChanged(nameof(DomainId));
            }
        }
        public int ContainerId 
        { 
            get => container_Id;
            set
            {
                container_Id = value;
                OnPropertyChanged(nameof(ContainerId));
                    
            }
        }

        public ObservableCollection<GroupModel> GroupList 
        {
            get
            {
                if(groupList == null)
                    groupList = new ObservableCollection<GroupModel>();
                return groupList;
            }
            set
            {
                groupList = value;
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
                List<ContainerScopeRelationModel> groupids = CommonResources.Container_Scope_Relation.Where(x => x.User_id == SelectedModelUser.Id).ToList();
                List<ContainerScopeModel> scopeids = CommonResources.Container_Scope.Where(x => x.Parent_id == SelectedModelUser.Id).ToList();


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
                var userList = CommonResources.User.Where(d => d.Domain_name == Domain.Domain).ToList();
                var groupList = CommonResources.Groups.Where(d => d.Domain_name == Domain.Domain).ToList();
                UserList = new ObservableCollection<UserModel>(userList);
                GroupList = new ObservableCollection<GroupModel>(groupList);
            }
            else
            {
                UserList = CommonResources.User;
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
