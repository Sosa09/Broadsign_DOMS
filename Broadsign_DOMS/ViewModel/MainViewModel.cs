using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Broadsign_DOMS.ViewModel
{
    public class MainViewModel : ObservableObject, IPageViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _listPageViewModel; 
        public IPageViewModel CurrentPageViewModel 
        { 
            get => _currentPageViewModel; 
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
            
        }

        public List<IPageViewModel> ListPageViewModel 
        { 
            get
            {
                if (_listPageViewModel == null)
                    _listPageViewModel = new List<IPageViewModel>();
                return _listPageViewModel;
            }
        }

        public MainViewModel()
        {
            ListPageViewModel.Add(new LoginViewModel());
            ListPageViewModel.Add(new HomeViewModel());
            ListPageViewModel.Add(new AdminViewModel());
            ListPageViewModel.Add(new ProblemViewModel());
            CurrentPageViewModel = ListPageViewModel[0];

            Mediator.Subscribe("HomeViewModel", HVMClicked);
            Mediator.Subscribe("ProblemViewModel", ProblemClicked);
            Mediator.Subscribe("AdminViewModel", AdminClicked);
        }

        private void AdminClicked(object obj)
        {
            changeViewModel(ListPageViewModel[2]);
        }

        private void ProblemClicked(object obj)
        {
            changeViewModel(ListPageViewModel[3]);
        }

        private void HVMClicked(object obj)
        {
            changeViewModel(ListPageViewModel[1]);
        }
        private void changeViewModel(IPageViewModel pageViewModel)
        {
            if (ListPageViewModel == null)
                ListPageViewModel.Add(pageViewModel);
            CurrentPageViewModel = pageViewModel;
            ListPageViewModel.First(vm => vm == pageViewModel);
        }
    }
}
