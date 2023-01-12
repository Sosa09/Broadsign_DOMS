using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Broadsign_DOMS.ViewModel
{
    public class UserViewModel : ObservableObject, IPageViewModel
    {
        private Domains domain;
        private ObservableCollection<UserModel> userList;
        private List<int> groupids;
        private UserModel selectedModelUser;
        private ObservableCollection<CloneUserModel> cloneUserModel;
        private string test_name;
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

        public UserModel SelectedModelUser 
        { 
            get => selectedModelUser;
            set
            {
                selectedModelUser = value;
                OnPropertyChanged(nameof(SelectedModelUser));
                _loadRelations();
            }
        }

        public ObservableCollection<CloneUserModel> CloneUserModel 
        {
            get
            {
                if (cloneUserModel == null)
                    cloneUserModel = new ObservableCollection<CloneUserModel>();
               return cloneUserModel;
            }
            set
            {
                cloneUserModel = value;
                OnPropertyChanged(nameof(CloneUserModel));
            }
        }

        public string Test_name 
        { 
            get => test_name;
            set
            {
                test_name = value;
                OnPropertyChanged(nameof(Test_name));
            }
        }

        private void _loadRelations()
        {
            dynamic user_groups = UserGroupModel.GetUserGroups(Domain.Token, SelectedModelUser.Id);
            foreach(var ugroup in user_groups["user_group"])
            {
                if(ugroup.active == true)
                {
                    groupids = new List<int>
                    {
                        
                         (int)ugroup.group_id
                    };
                }
            }
            CloneUserModel.Add(
                new Model.CloneUserModel 
                { 
                    Group_ids = groupids, 
                    Id = SelectedModelUser.Id, 
                    Name = SelectedModelUser.Name, 
                    Username = SelectedModelUser.Username, 
                    UserContainer_id = SelectedModelUser.Container_id 
                }
           );

            
            //dynamic user_group_scoping_relation = ContainerScopeRelation.GetScopingRelation(Domain.Token);
            //foreach (var ugsRelation in user_group_scoping_relation["container_scope_relationship"])
            //{
            //    if (ugsRelation.active == true)
            //    {
                    
            //    }
            //}
        }

        private void _generateList()
        {
            dynamic users = UserModel.getUser(Domain.Token);
            if(users != null)
            {
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
