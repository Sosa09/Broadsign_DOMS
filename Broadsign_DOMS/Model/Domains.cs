using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class Domains : ObservableObject
    {
        private string domain;
        private string userName;
        private string token;

        public string Domain 
        {
            get
            {
                return domain;
            }
            set
            {
                domain = value;
                OnPropertyChanged(Domain);
            }
        }
        public string UserName 
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(UserName);
            }
        }
        public string Token 
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                OnPropertyChanged(Token);
            }
        }


    }
}
