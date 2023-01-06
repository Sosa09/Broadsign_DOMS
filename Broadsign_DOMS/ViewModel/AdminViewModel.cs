using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class AdminViewModel : ObservableObject, IPageViewModel
    {
        ICommand backButtonCommand;
        ICommand executeButtonCommand;
        IPageViewModel currentMenu;
        private ObservableCollection<Domains> domainList;
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
        Domains selectedDomain;
        

        public AdminViewModel()
        {
            CurrentMenu = new UserViewModel();
        }

        public ICommand BackButtonCommand 
        {
            get
            {
                return backButtonCommand ?? (new RelayCommand(x => Mediator.Notify("HomeViewModel", "")));
            }
            
        }
        public ICommand ExecuteButtonCommand
        {
            get
            {
                //request api return result
                return executeButtonCommand ?? (new RelayCommand(x => {
                    if (SelectedDomain != null) MessageBox.Show(SelectedDomain.Token);
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
            
                
            }

        }

        

        
    }
}
