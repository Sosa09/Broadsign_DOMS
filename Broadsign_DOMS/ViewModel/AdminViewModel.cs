﻿using Broadsign_DOMS.Model;
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
        ICommand backButtonCommand;
        ICommand executeButtonCommand;
        ICommand viewClickedCommand;
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
                    if (CurrentMenu == null || SelectedDomain == null)
                    {
                        MessageBox.Show("Please select a domain, and make sure a view menu is selected !");
                        return;
                    }
                    _sendMsg(SelectedDomain);

                }));
            }
        }
        private void _myCommandParam(string cmdParam)
        {
            MessageBox.Show(cmdParam);
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
        public ICommand ViewClickedCommand 
        {
            get
            {
                if (viewClickedCommand == null)
                    viewClickedCommand = new RelayCommand<string>(_myCommandParam);
                return viewClickedCommand;
            }
           
        }

        private void _sendMsg(Domains d)
        {
            Messenger.Default.Send(d);

        }

        
    }
}
