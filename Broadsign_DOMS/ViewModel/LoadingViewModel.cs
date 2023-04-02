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

namespace Broadsign_DOMS.ViewModel
{
    public class LoadingViewModel : ObservableObject, IPageViewModel
    {
        private ObservableCollection<Domains> _listDomains;
        private Domains domains = new Domains();
        private List<string> _loaded = new List<string>();
        private string _loadingMessage;

        public LoadingViewModel()
        {
            generateObsColl();
            _loadingSync();
        }
        public ObservableCollection<Domains> ListDomains
        {
            get
            {
                if (_listDomains == null)
                {
                    _listDomains = domains.DomainList;
                }
                return _listDomains;
            }
            set
            {
                _listDomains = value;
                OnPropertyChanged(nameof(ListDomains));
            }
        }

        public string LoadingMessage 
        {
            get
            {
                return _loadingMessage;
            }
            set
            {
                _loadingMessage = value;
                OnPropertyChanged(nameof(LoadingMessage));
            }
        }

        private void generateObsColl()
        {
            //instantiate all observableobject from tyhe commonresources class to store the api results

            CommonResources.Players = new ObservableCollection<PlayerModel>();
            CommonResources.DisplayUnits = new ObservableCollection<DisplayUnitModel>();
            CommonResources.Frames = new ObservableCollection<FrameModel>();
            CommonResources.DayParts = new ObservableCollection<DayPartModel>();
            CommonResources.Users = new ObservableCollection<UserModel>();
            CommonResources.Containers = new ObservableCollection<ContainerModel>();
            CommonResources.Groups = new ObservableCollection<GroupModel>();
            CommonResources.Container_Scopes = new ObservableCollection<ContainerScopeModel>();
            CommonResources.Container_Scope_Relations = new ObservableCollection<ContainerScopeRelationModel>();


        }
        private async Task _loadAllBaseResources(Domains domain)
        {
       
            LoadingMessage += $"\nLoading broadsign 'PLAYERS' for domain {domain.Domain}";
            await PlayerModel.GeneratePlayers(domain.Token);
            LoadingMessage += $"\n{CommonResources.Players.Count} 'PLAYERS' for domain {domain.Domain} loaded";


            LoadingMessage += $"\nLoading broadsign 'USER' for domain {domain.Domain}";
            try
            {
                dynamic users = UserModel.GetUsers(domain.Token);
                //extract users
                if (users != null)
                {
                    //show message loading resource for country ...

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
                                Domain_name = domain.Domain

                            });
                        }


                    }
                }

            }catch(Exception e)
            {
                LoadingMessage += $"REQUEST FAILURE: {domain.Domain} Failed to get user's api request";
            }
            finally
            {
                await Task.Delay(1);
                LoadingMessage += $"\n{CommonResources.Users.Count} 'GROUPS' for domain {domain.Domain} loaded";
            }

            await Task.Delay(1);
            //extract userGroups
            LoadingMessage += $"\n Loading broadsign 'GROUP' resources for domain {domain.Domain}";
            try
            {
                dynamic groups = GroupModel.GetGroups(domain.Token);
                if (groups != null)
                {
                    //show message loading resource for country ...
                    foreach (var group in groups["group"])
                    {
                        CommonResources.Groups.Add(new GroupModel
                        {
                            Active = group.active,
                            Domain_id = group.domain_id,
                            Container_id = group.container_id,
                            Id = group.id,
                            Name = group.name,
                            Domain_name = domain.Domain
                        });
                    }

                }

            }
            catch (Exception e)
            {
                await Task.Delay(1);
                LoadingMessage += $"REQUEST FAILURE: {domain.Domain} Failed to get group's api request";
            }
            finally
            {
                await Task.Delay(1);
                LoadingMessage += $"\n{CommonResources.Groups.Count} 'Groups' for domain {domain.Domain} loaded";

            }

            //extract containers
            await Task.Delay(1);
            LoadingMessage += $"\nLoading broadsign 'CONTAINERS' for Domain: {domain.Domain}";
            try
            {
                dynamic containers = ContainerModel.GetContainers(domain.Token);
                if (containers != null)
                {
                    foreach (var container in containers["container"])
                    {
                        //show message loading resource for country ...
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
            }catch(Exception e)
            {
                await Task.Delay(1);
                LoadingMessage += $"REQUEST FAILURE: {domain.Domain} Failed to get Container's api request";
            }
            finally
            {
                await Task.Delay(1);
                LoadingMessage += $"\n{CommonResources.Groups.Count} 'CONTAINERS' Loaded for domain {domain.Domain}";
            }

            

            //extract container scope
            await Task.Delay(1);
            
            LoadingMessage += $"\n Loading broadsdign 'CONTAINER SCOPE' for domain {domain.Domain}";
            try
            {
                dynamic scopes = ContainerScopeModel.GetContainerScopes(domain.Token);

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
            }catch (Exception e)
            {
                await Task.Delay(1);
                LoadingMessage += $"REQUEST FAILURE: {domain.Domain} Failed to get container_scope's api request";
            }
            finally
            {
                await Task.Delay(1);
                LoadingMessage += $"\n'CONTAINER SCOPE' for domain {domain.Domain} Successfully loaded";
            }



            //extract CONTAINER SCOPE RELATIONS
            await Task.Delay(1);
            LoadingMessage += $"\n Loading broadsdign 'CONTAINER SCOPE RELATIONS' for domain {domain.Domain}";
            try
            {
                dynamic relation_users_containers = ContainerScopeRelationModel.GetScopingRelation(domain.Token);

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
            catch (Exception e)
            {
                await Task.Delay(1);
                LoadingMessage += $"REQUEST FAILURE: {domain.Domain} Failed to get container_scope's api request";
            }
            finally
            {
                await Task.Delay(1);
                LoadingMessage += $"\n'CONTAINER SCOPE RELATIONS' for domain {domain.Domain} Successfully loaded" +
                    $"\n++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
            }



            
            

        }



        private async Task _loadingSync()
        {
            List<Task> tasks = new List<Task>();

            LoadingMessage += "START LOADING BS RESOURCES";
            //Go through all domains to get all resources
            foreach (var domain in ListDomains)
                await _loadAllBaseResources(domain);
           
            await Task.WhenAll(tasks);
            Mediator.Notify("HomeViewModel", "");
            Messenger.Default.Send(ListDomains, "HomeViewModel");
            
        }
    }
}
