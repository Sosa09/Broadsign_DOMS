using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        ICommand problemView;
        ICommand adminView;
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
                return adminView ?? (new RelayCommand(x => Mediator.Notify("AdminViewModel", "")));

            }
        }

        public HomeViewModel()
        {
       
        }


    }
}
