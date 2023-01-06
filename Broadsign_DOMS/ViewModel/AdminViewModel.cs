using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class AdminViewModel : ObservableObject, IPageViewModel
    {
        ICommand backButtonCommand;
        ICommand executeButtonCommand;

        ObservableCollection<Domains> domainList;
        Domains selectedDomain;

        public AdminViewModel()
        {
      
        }

        public ICommand BackButtonCommand 
        {
            get
            {
                return backButtonCommand ?? (new RelayCommand(x => Mediator.Notify("HomeViewModel", "")));
            }
            
        }

        public ICommand ExecuteButtonCommand
        {
            get
            {
                //request api return result
                return executeButtonCommand ?? (new RelayCommand(x => MessageBox.Show("")));
            }
        }

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

        public Domains SelectedDomain 
        {
            get
            {
   
                return selectedDomain;
            }
            set
            {
                selectedDomain = value;
                OnPropertyChanged(nameof(SelectedDomain));
            
                
            }

        }

        private void generateAllTokens()
        {
            domainList = new ObservableCollection<Domains>();
            using (StreamReader streamReader = new StreamReader(@"C:\Users\BECCO1SAR\source\repos\Broadsign_DOMS\Broadsign_DOMS\bin\Debug\net6.0-windows\api.csv"))
            {
                var line = streamReader;
                while(line.ReadLine() != null)
                {
                    string[]? l = line.ReadLine().Split(',');
                    domainList.Add(new Domains { Domain = l[0], UserName = l[1], Token = l[2] });
                }
                
            }
            
        }
    }
}
