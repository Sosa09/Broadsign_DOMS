using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class AdminViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        ICommand backButtonCommand;
        ICommand executeButtonCommand;
        ICommand viewClickedCommand;
        IPageViewModel currentMenu;

        Domain _selectedDomain;
        ObservableCollection<Domain> domainList;


        string currentView;
        string _search;
        #endregion

        #region Contructors
   
        #endregion

        #region Properties
        public ObservableCollection<Domain> DomainList
        {
            get
            {
                if (domainList == null)
                    domainList = new Domain().DomainList;
                return domainList;
            }
            set
            {
                domainList = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }
        public ICommand BackButtonCommand 
        {
            get
            {
                return backButtonCommand ?? (new RelayCommand(x =>
                {
                    
                    Mediator.Notify("HomeViewModel", "");
                }));
            }
            
        }
        public ICommand ExecuteButtonCommand
        {
            get
            {
                //request api return result
                return executeButtonCommand ?? (new RelayCommand(x => {
                    if (CurrentMenu == null)
                    {
                        MessageBox.Show("Please select a domain, and make sure a view menu is selected !");
                        return;
                    }
                    
                    
             

                }));
            }
        }
        public IPageViewModel CurrentMenu 
        { 
            get => currentMenu;
            set
            {
                currentMenu = value;
                OnPropertyChanged(nameof(CurrentMenu));
            }
        }
        public Domain SelectedDomain 
        {
            get => _selectedDomain;
            set
            {
                _selectedDomain = value;
                OnPropertyChanged(nameof(SelectedDomain)); 
                _sendMsg();


            }

        }
        public ICommand ViewClickedCommand 
        {
            get
            {
                if (viewClickedCommand == null)
                    viewClickedCommand = new RelayCommand<string>(_selectedViewModel);
                return viewClickedCommand;
            }
           
        }

        public string Search 
        { 
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged("Search");
                Messenger.Default.Send(_search, $"Search{currentView}");
            } 
        }
        #endregion

        #region Methods
        private void _selectedViewModel(string cmdParam)
        {
            if (cmdParam == "UserViewModel")
                CurrentMenu = new UserViewModel();
            else if (cmdParam == "GroupModel")
                CurrentMenu = new GroupViewModel();
            else if (cmdParam == "ConfigProfileModel")
                CurrentMenu = new ConfigProfileViewModel();
            else if (cmdParam == "ResourceViewModel")
                CurrentMenu = new ResourceViewModel();
            else if (cmdParam == "OpeningHoursModel")
                CurrentMenu = new OpeningHoursViewModel();
            else
                MessageBox.Show("Problem");
            currentView = cmdParam;

            _sendMsg();

        }
        private void _sendMsg(string d = null)
        {
            if (SelectedDomain != null)
                d = SelectedDomain.Name;
            Messenger.Default.Send(d, $"Domain{currentView}");
        }
        #endregion
    }
}