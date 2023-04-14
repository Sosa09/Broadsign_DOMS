using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    //TODO: how to establish a connection with HOSTNAME TYPED OR Selected HOSTNAME
    public class ProblemViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _remoteOptionsCommand;
        private ICommand _connectSshCommand;
        private ICommand _connectScpCommand;

        private SshOptions _sshSession;
        private PlayerModel _selectedPlayer;
        private ObservableCollection<PlayerModel> _playerList;
        private ObservableCollection<SshOptions> _activeSessions;

        private IEnumerable<string> _domainList;

        private string _hostName;
        private bool _isConnected;
        private string _status;
        private string _result;
        private string _selectedDomain;


        public ObservableCollection<SshOptions> ActiveSessions
        {
            get 
            {
                if(_activeSessions == null)
                {
                    _activeSessions = new ObservableCollection<SshOptions>();
                    _activeSessions.Add(_sshSession = new SshOptions() { HostName = "hello" });

                }
                return _activeSessions;
            }
            set
            {
                _activeSessions = value;
                OnPropertyChanged("ActiveSessions");
            }
        }
        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
                OnPropertyChanged("HostName");
                //PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.Name.Contains(_hostName) && x.Domain.Name == SelectedDomain));
            }

        }
        public string Status
        {
            get
            {

                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));

            }
        }
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public ICommand RemoteOptionsCommand
        {
            get
            {
                return _remoteOptionsCommand ?? (new RelayCommand(
                    _executeRemoteCommand,
                    param => IsConnected
                    ));
            }
        }

        public ICommand ConnectSshCommand
        {
            get
            {
                return _connectSshCommand ?? (new RelayCommand(param => _sshConnection()));
            }
        }

        public ICommand ConnectScpCommand
        {
            get
            {
                return _connectScpCommand ?? (new RelayCommand(param => _scpConnection()));
            }
        }

        public ObservableCollection<PlayerModel> PlayerList
        {
            get
            {

                return _playerList ?? new ObservableCollection<PlayerModel>();
            }
            set
            {
                _playerList = value;
                OnPropertyChanged("PlayerList");
            }
        }
        public string SelectedDomain
        {
            get => _selectedDomain;
            set
            {
                _selectedDomain = value;
                OnPropertyChanged("SelectedDomain");
                PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.AssignedDomain.Name == _selectedDomain));
            }
        }


        public IEnumerable<string> DomainList
        {
            get
            {
                if (_domainList == null)
                    _domainList = CommonResources.Players.Select(x => x.AssignedDomain.Name).Distinct();
                return _domainList;
            }
            set
            {
                _domainList = value;
                OnPropertyChanged("DomainList");
            }
        }

        public PlayerModel SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                //TODO: set a maximum of 5 selected players and connecitons
                _selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }

        private void _scpConnection()
        {
            if (_checkHostNameIsValid())
            {
                _sshSession.StartScpSession();
            }

        }

        private void _executeRemoteCommand(object param)
        {
            //declare a command var
            string cmd = "";
            if ((string)param == "reboot")
                cmd = "sudo reboot";
            else if ((string)param == "xrandr get")
                cmd = "xrandr --verbose";
            else if ((string)param == "Remove Selected Files")
                cmd = "";//will be selected files from a list
            else if ((string)param == "process")
                cmd = "ps -aux";
            else if ((string)param == "consul")
                cmd = "sudo /opt/configuration/converge.sh checknow";
            Result = _sshSession.ExecuteCommand(cmd);
        }

        private void _sshConnection()
        {

            //show ssh connections into the ldatagrid

            _sshSession = new SshOptions
            {
                HostName = HostName
            };
            _sshSession.StartSshSession();
            
            ActiveSessions.Add(_sshSession);
     
            
        }
        private bool _checkHostNameIsValid(string selected = "")
        {
        
            if (!Regex.IsMatch(selected.Trim(), "^[A-Za-z]{2}-[A-Za-z]{2}-[A-Za-z]{3}-[A-Za-z0-9]{4}$"))
            {
                MessageBox.Show("please enter a valid hostname e.g: UK-OT-LON-P001");
                return false;
            }
            //assign hostname to local host var in sshoptions
            _sshSession.HostName = selected;
            return true;
        }
    }
}
