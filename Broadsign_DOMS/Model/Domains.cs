using Broadsign_DOMS.Service;
using System.Collections.ObjectModel;
using System.IO;

namespace Broadsign_DOMS.Model
{
    public class Domain : ObservableObject
    {
        private string _name;
        private string userName;
        private string token;

        ObservableCollection<Domain> domainList;
        public ObservableCollection<Domain> DomainList
        {
            get
            {
                _generateAllTokens();
                return domainList;
            }
            set
            {
                domainList = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }

        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(Name);
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

        private void _generateAllTokens()
        {
            domainList = new ObservableCollection<Domain>();

            using (StreamReader streamReader = new StreamReader(@"api.csv"))
            {
                var line = streamReader;         
                while (!line.EndOfStream)
                {
               
            
                        string[]? l = line.ReadLine().Split(',', ';');
                        domainList.Add(new Domain { Name = l[0], Token = l[1] });
  
                }

            }

        }
    }
}
