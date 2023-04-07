using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
                OnPropertyChanged("LoadingMessage");
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
            //TODO check if not better to put this in tasks
            //LoadingMessage += $"\n\nLoading broadsign 'PLAYERS' for domain {domain.Domain}";
            //await PlayerModel.GeneratePlayers(domain.Token);
            //LoadingMessage += $"\n{CommonResources.Players.Count} 'PLAYERS' for domain {domain.Domain} loaded";

            //LoadingMessage += $"\n\nLoading broadsign 'FRAMES' for domain {domain.Domain}";
            //await FrameModel.GenerateFrames(domain.Token);
            //LoadingMessage += $"\n{CommonResources.Frames.Count} 'FRAMES' for domain {domain.Domain} loaded";

            //LoadingMessage += $"\n\nLoading broadsign 'DISPLAY UNITS' for domain {domain.Domain}";
            //await DisplayUnitModel.GenerateDisplayUnits(domain.Token);
            //LoadingMessage += $"\n{CommonResources.DisplayUnits.Count} 'DISPLAY UNITS' for domain {domain.Domain} loaded";

            //LoadingMessage += $"\n\nLoading broadsign 'USERS' for domain {domain.Domain}";
            //await UserModel.GenerateUsers(domain.Token);
            //LoadingMessage += $"\n{CommonResources.Users.Count} 'USERS' for domain {domain.Domain} loaded";

            //LoadingMessage += $"\n\n Loading broadsign 'GROUPS' resources for domain {domain.Domain}";
            //await GroupModel.GenerateGroups(domain.Token);
            //LoadingMessage += $"\n{CommonResources.Groups.Count} 'GROUPS' for domain {domain.Domain} loaded";

            //LoadingMessage += $"\n\nLoading broadsign 'CONTAINERS' for Domain: {domain.Domain}";
            //await ContainerModel.GenerateContainers(domain.Token);
            //LoadingMessage += $"\n{CommonResources.Groups.Count} 'CONTAINERS' Loaded for domain {domain.Domain}";

            //LoadingMessage += $"\n\nLoading broadsign 'CONTAINER SCOPE' for Domain: {domain.Domain}";
            //await ContainerScopeModel.GeneratContainerScopes(domain.Token);
            //LoadingMessage += $"\n'CONTAINER SCOPE' for domain {domain.Domain} Successfully loaded";

            //LoadingMessage += $"\n Loading broadsdign 'CONTAINER SCOPE RELATIONS' for domain {domain.Domain}";
            //await ContainerScopeRelationModel.GenerateScopingRelations(domain.Token);
            //LoadingMessage += $"\n'CONTAINER SCOPE RELATIONS' for domain {domain.Domain} Successfully loaded" +
            //                  $"\n++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";

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
            File.Create(".\\text.txt");
            File.AppendText(LoadingMessage);
        }
    }
}
