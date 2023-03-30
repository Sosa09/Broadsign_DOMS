using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        ICommand problemView;
        ICommand adminView;
        ObservableCollection<Domains> domainList;
        private bool _successFullLogin;

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

        //public bool _SuccessFullLogin 
        //{ 
        //    get => _successFullLogin;
        //    set
        //    {
        //        _successFullLogin = value;
        //        OnPropertyChanged(nameof(_SuccessFullLogin));
        //        if (value == true)
        //            _loadAllBaseResources();
        //    }
        //}

        public HomeViewModel()
        {
       
            //Messenger.Default.Register<bool>(this,"HomeViewModel", x => _SuccessFullLogin = x, true);
   

        }

    }
}
