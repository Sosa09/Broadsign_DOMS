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
        private IPageViewModel currentTemplate;

        private ObservableCollection<Domains> listDomains;
        #endregion
        #region Constructors
        public StartViewModel()
        {
            
            CurrentTemplate = new LoginViewModel();

            
        }
        #endregion
        #region Properties
        public IPageViewModel CurrentTemplate
        {
            get => currentTemplate;
            set
            {
                currentTemplate = value;
                OnPropertyChanged(nameof(CurrentTemplate));
            }
        }

        public ObservableCollection<Domains> ListDomains 
        { 
            get
            {
                return listDomains == null ? listDomains = Domains.
            }
            set
            {
                listDomains = value;
                OnPropertyChanged(nameof(ListDomains));
            }
        }


        #endregion
    }
}