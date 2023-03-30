using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class LoginViewModel : ObservableObject, IPageViewModel
    {
        private string username;
        private ICommand loginButtonCommand;

        public string Username 
        {

            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(Username);
            }
        }
        //Add login form
        public ICommand LoginButtonCommand 
        {
            get
            {
                return loginButtonCommand ?? (new RelayCommand(x =>
                {
                    Mediator.Notify("HomeViewModel", "");
                    Messenger.Default.Send(true,"HomeViewModel");
                })); 
            }
        }
    }
}
