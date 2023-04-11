using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        ICommand problemView;
        ICommand adminView;
        public ObservableCollection<Domain> ListDomains { get; set; }
   

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


        public HomeViewModel()
        {
       
            Messenger.Default.Register<ObservableCollection<Domain>>(this,"HomeViewModel", x => ListDomains = x, true);
   

        }

    }
}
