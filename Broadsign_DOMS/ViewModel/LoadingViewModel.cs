using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.ViewModel
{
    public class LoadingViewModel : ObservableObject, IPageViewModel
    {

        public LoadingViewModel()
        {

        }     

        private void _loadAllBaseResources()
        {

            //instantiate all observableobject from tyhe commonresources class to store the api results
            CommonResources.Users = new ObservableCollection<UserModel>();
            CommonResources.Groups = new ObservableCollection<GroupModel>();
            CommonResources.Containers = new ObservableCollection<ContainerModel>();
            CommonResources.Container_Scopes = new ObservableCollection<ContainerScopeModel>();
            CommonResources.Container_Scope_Relations = new ObservableCollection<ContainerScopeRelationModel>();


            //Go through all domains to get all resources
            foreach (var token in DomainList)
            {
                //get all apis from all domains resource Users, UserGroups, Players, Containers, Players, Frames(Skin), Day_Parts,
                dynamic users = UserModel.GetUsers(token.Token);
                dynamic groups = GroupModel.GetGroups(token.Token);
                dynamic containers = ContainerModel.GetContainers(token.Token);
                dynamic scopes = ContainerScopeModel.GetContainerScopes(token.Token);
                dynamic relation_users_containers = ContainerScopeRelationModel.GetScopingRelation(token.Token);

                //extract users from json variable and store then in Commonresources.User
                if (users != null)
                {

                    foreach (var user in users["user"])
                    {
                        if (user.active == true)
                        {
                            CommonResources.Users.Add(new UserModel
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

                //extract userGroups from json variable and store then in Commonresources.UserGroups
                if (groups != null)
                {
                    foreach (var group in groups["group"])
                    {
                        CommonResources.Groups.Add(new GroupModel
                        {
                            Active = group.active,
                            Domain_id = group.domain_id,
                            Container_id = group.container_id,
                            Id = group.id,
                            Name = group.name,
                            Domain_name = token.Domain
                        });
                    }

                }
                //extract containers from json variable and store then in Commonresources.User
                if (containers != null)
                {
                    foreach (var container in containers["container"])
                    {
                        if (container.active == true)
                        {
                            CommonResources.Containers.Add(new ContainerModel
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

                //extract users from json variable and store then in Commonresources.User
                if (scopes != null)
                {
                    foreach (var container_scope in scopes["container_scope"])
                    {
                        if (container_scope.active == true)
                        {
                            CommonResources.Container_Scopes.Add(new ContainerScopeModel
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

                //extract users from json variable and store then in Commonresources.User
                if (relation_users_containers != null)
                {
                    foreach (var ugsRelation in relation_users_containers["container_scope_relationship"])
                    {
                        if (ugsRelation.active == true)
                        {
                            CommonResources.Container_Scope_Relations.Add(new ContainerScopeRelationModel
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
