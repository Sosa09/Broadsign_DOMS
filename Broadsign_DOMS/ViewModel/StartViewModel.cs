using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.ViewModel
{
    public class StartViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IPageViewModel _currentTemplate;
        private ObservableCollection<Domain> _listDomains;
        private bool _successfullLogin;
        #endregion

        #region Constructors
        public StartViewModel()
        {            
            CurrentTemplate = new LoginViewModel();
            Messenger.Default.Register<bool>(this, "StartViewModel",b => SuccessfullLogin = b, true);            
        }
        #endregion

        #region Properties
        public IPageViewModel CurrentTemplate
        {
            get => _currentTemplate;
            set
            {
                _currentTemplate = value;
                OnPropertyChanged(nameof(CurrentTemplate));
            }
        }
        public ObservableCollection<Domain> ListDomains 
        { 
            get
            {
                if (_listDomains == null )
                    _listDomains = new Domain().DomainList;
                return _listDomains;
            }
            set
            {
                _listDomains = value;
                OnPropertyChanged(nameof(ListDomains));
            }
        }
        public bool SuccessfullLogin 
        { 
            get => _successfullLogin;
            set
            {
                _successfullLogin = value;
                OnPropertyChanged(nameof(SuccessfullLogin));
                CurrentTemplate = new LoadingViewModel();
            }

        }
        #endregion
    }
}