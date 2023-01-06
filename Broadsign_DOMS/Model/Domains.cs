﻿using Broadsign_DOMS.Service;
using System.Collections.ObjectModel;
using System.IO;

namespace Broadsign_DOMS.Model
{
    public class Domains : ObservableObject
    {
        private string domain;
        private string userName;
        private string token;
        ObservableCollection<Domains> domainList;
        public ObservableCollection<Domains> DomainList
        {
            get
            {
                generateAllTokens();
                return domainList;
            }
            set
            {
                domainList = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }

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

        private void generateAllTokens()
        {
            domainList = new ObservableCollection<Domains>();
            using (StreamReader streamReader = new StreamReader(@"C:\Users\BECCO1SAR\source\repos\Broadsign_DOMS\Broadsign_DOMS\bin\Debug\net6.0-windows\api.csv"))
            {
                var line = streamReader;
                while (line.ReadLine() != null)
                {
                    string[]? l = line.ReadLine().Split(',');
                    domainList.Add(new Domains { Domain = l[0], UserName = l[1], Token = l[2] });
                }

            }

        }
    }
}
