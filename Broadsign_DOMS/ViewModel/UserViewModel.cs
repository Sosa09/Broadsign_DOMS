using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Broadsign_DOMS.ViewModel
{
    public class UserViewModel : ObservableObject, IPageViewModel
    {
        private Domains domain;
        private ObservableCollection<UserModel> userList;
        private ObservableCollection<ContainerScopeRelationModel> containerScopeRelations;
        private ObservableCollection<ContainerScopeModel> containerScopes;
        private ObservableCollection<ContainerModel> containers;
        private UserModel selectedModelUser;
        private CloneUserModel cloneUserModel = new CloneUserModel();
    
        public UserViewModel()
        {
            
            Messenger.Default.Register<Domains>(this, "UserViewModel", message => Domain = message);
            
        }

        public Domains Domain 
        { 

            get => domain;
            set
            {
                domain = value;
                OnPropertyChanged(nameof(domain));
                _generateList();
                _loadAdditionalResources();
            }
        }

        public ObservableCollection<UserModel> UserList 
        {
            get
            {
                
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

        private void _searchRelation()
        {
            ////TODO Convert foreach into linqreturns 0
            List<ContainerScopeRelationModel> groupids = ContainerScopeRelations.Where(x => x.User_id == SelectedModelUser.Id).ToList();
            List<ContainerScopeModel> scopeids = ContainerScopes.Where(x => x.Parent_id == SelectedModelUser.Id).ToList();
     

            CloneUserModel.Group_ids = groupids;
            CloneUserModel.ScopingRelation_ids = scopeids;
            CloneUserModel.Id = SelectedModelUser.Id;
            CloneUserModel.Name = SelectedModelUser.Name;
            CloneUserModel.Username = SelectedModelUser.Username;
            CloneUserModel.UserContainer_id = SelectedModelUser.Container_id;


            Messenger.Default.Send(CloneUserModel, "AdminViewModel");
        }
        private void _loadAdditionalResources()
        {
            //get all containers for all resources store them even for the other menu items of admin view (in case you need to check or modify something for the same domain)
            //getting all relations between users and user groups
            //then extract all assigned container scope and find the relations by the parent id of this item (parent id can be a user or a user group in this case)
            dynamic api_containers = ContainerModel.GetContainers(Domain.Token);
            dynamic api_user_group_scoping_relation = ContainerScopeRelationModel.GetScopingRelation(Domain.Token);
            dynamic api_container_scope = ContainerScopeModel.GetContainerScopes(Domain.Token);
            foreach (var ugsRelation in api_user_group_scoping_relation["container_scope_relationship"])
            {
                if (ugsRelation.active == true)
                {
                    ContainerScopeRelations.Add(new ContainerScopeRelationModel
                    {
                        Active = ugsRelation.active,
                        Domain_id = ugsRelation.domain_id,
                        Id = ugsRelation.id,
                        Parent_id = ugsRelation.parent_id,
                        User_id = ugsRelation.user_id
                    });
                }
            }
            foreach(var container_scope in api_container_scope["container_scope"])
            {
                if (container_scope.active == true)
                {
                    ContainerScopes.Add(new ContainerScopeModel
                    {
                        Active = container_scope.active,
                        Can_see_above = container_scope.can_see_above,
                        Domain_id = container_scope.domain_id,
                        Id = container_scope.id,
                        Parent_id = container_scope.parent_id,
                        Scope_container_group_id = container_scope.scope_container_group_id,
                        Scope_container_id = container_scope.scope_container_id,
                        Scope_resource_type = container_scope.scope_resource_type,
                    });
                }
            }
            foreach(var container in api_containers["container"])
            {
                if(container.active == true)
                {
                    Containers.Add(new ContainerModel
                    {
                        Active = container.active,
                        Container_id = container.container_id,
                        Domain_id = container.domain_id,
                        Group_id = container.group_id,
                        Id = container.id,
                        Name = container.name,
                        Parent_id = container.parent_id,
                        Parent_resource_type = container.parent_resource_type
                       
                    });
                }
            }

        }

        private void _generateList()
        {
            dynamic users = UserModel.getUser(Domain.Token);
            if(users != null)
            {
                if (userList.Count > 0)
                    userList.Clear();
                foreach(var user in users["user"])
                {
                    if (user.active == true)
                    {
                        userList.Add(new UserModel
                        {
                            Active = user.active,
                            Allow_auth_token = user.allow_auth_token,
                            Container_id = user.container_id,
                            Domain_id = user.domain_id,
                            Email = user.email,
                            Has_auth_token = user.has_auth_token,
                            Id = user.id,
                            Name = user.name,
                            Passwd = user.password,
                            Pending_single_sign_on_email = user.pending_single_sign_on_email,
                            Public_key_fingerprint = user.public_key_fingerprint,
                            Single_sign_on_id = user.single_sign_on_id,
                            Username = user.username
                        });
                    }

 
                }
            }
        }


    }
}
