using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        ICommand problemView;
        ICommand adminView;
        ObservableCollection<Domains> domainList;
        CommonResources cr;
        public ICommand ProblemView
        {
            get
            {
                return problemView ?? (new RelayCommand(x =>
                {
                    Mediator.Notify("ProblemViewModel", "");
                }));
            }
        }

        public ICommand AdminView
        {
            get
            {
                return adminView ?? (new RelayCommand(x =>
                {
                    Mediator.Notify("AdminViewModel", "");
              
                }));

            }
        }

        public ObservableCollection<Domains> DomainList
        {
            get
            {
                if (domainList == null)
                    domainList = new Domains().DomainList;
                return domainList;
            }
            set
            {
                domainList = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }

        public HomeViewModel()
        {


            CommonResources.User = new ObservableCollection<UserModel>();
            CommonResources.Container = new ObservableCollection<ContainerModel>();
            CommonResources.Container_scope = new ObservableCollection<ContainerScopeModel>();
            CommonResources.Container_scope_relation = new ObservableCollection<ContainerScopeRelationModel>();
            foreach (var token in DomainList)
            {
            
                dynamic users = UserModel.GetUsers(token.Token);
                dynamic containers = ContainerModel.GetContainers(token.Token);
                dynamic scopes = ContainerScopeModel.GetContainerScopes(token.Token);
                dynamic relation_users_containers = ContainerScopeRelationModel.GetScopingRelation(token.Token);
                if (users != null)
                {

                    foreach (var user in users["user"])
                    {
                        if (user.active == true)
                        {
                            CommonResources.User.Add(new UserModel
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
                                Username = user.username,
                                Domain_name = token.Domain

                            });
                        }


                    }
                }
                if (containers != null)
                {
                    foreach (var container in containers["container"])
                    {
                        if (container.active == true)
                        {
                            CommonResources.Container.Add(new ContainerModel
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
                if(scopes != null) 
                {
                    foreach (var container_scope in scopes["container_scope"])
                    {
                        if (container_scope.active == true)
                        {
                            CommonResources.Container_scope.Add(new ContainerScopeModel
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
                }
                if (relation_users_containers != null)
                {
                    foreach (var ugsRelation in relation_users_containers["container_scope_relationship"])
                    {
                        if (ugsRelation.active == true)
                        {
                            CommonResources.Container_scope_relation.Add(new ContainerScopeRelationModel
                            {
                                Active = ugsRelation.active,
                                Domain_id = ugsRelation.domain_id,
                                Id = ugsRelation.id,
                                Parent_id = ugsRelation.parent_id,
                                User_id = ugsRelation.user_id
                            });
                        }
                    }
                }

            }


        }
    }
}
