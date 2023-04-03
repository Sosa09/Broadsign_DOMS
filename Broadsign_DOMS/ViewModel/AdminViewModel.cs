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

        Domains selectedDomain;
        ObservableCollection<Domains> domainList;
        CloneUserModel viewSelectedItem;

        string currentView;
        #endregion

        #region Contructors
        public AdminViewModel()
        {

            Messenger.Default.Register<CloneUserModel>(this, "AdminViewModel", message => ViewSelectedItem = message, true);
     

        }
        #endregion

        #region Properties
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
        public Domains SelectedDomain 
        {
            get
            {
   
                return selectedDomain;
            }
            set
            {
                selectedDomain = value;
                OnPropertyChanged(nameof(SelectedDomain)); 
                _sendMsg(SelectedDomain);


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

        public CloneUserModel ViewSelectedItem 
        {
            get
            {
         
                return viewSelectedItem;
            }
            set
            {
                viewSelectedItem = value;
                OnPropertyChanged(nameof(ViewSelectedItem));
              
            }
        }
        #endregion

        #region Methods
        private void _selectedViewModel(string cmdParam)
        {
            if (cmdParam.ToLower() == "UserViewModel")
                CurrentMenu = new UserViewModel();
            else if (cmdParam == "GroupModel")
                CurrentMenu = new GroupViewModel();
            else if (cmdParam == "ConfigProfileModel")
                CurrentMenu = new ConfigProfileViewModel();
            else if (cmdParam == "ResourceModel")
                CurrentMenu = new ResourceViewModel();
            else if (cmdParam == "OpeningHoursModel")
                CurrentMenu = new UserViewModel();
            else
                MessageBox.Show("Problem");
            currentView = cmdParam;

        }
        private void _sendMsg(Domains d)
        {
            Messenger.Default.Send(d, currentView);

        }
        #endregion
    }
}