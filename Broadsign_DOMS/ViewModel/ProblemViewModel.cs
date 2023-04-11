using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private SshOptions _sshOptions;
        private PlayerModel _selectedPlayer;
        private ObservableCollection<PlayerModel> _playerList;
        private ObservableCollection<object> _players;

        private IEnumerable<string> _domainList;

        private string _hostName;
        private bool _isConnected;
        private string _status;
        private string _result;
        private string _selectedDomain;


        private ObservableCollection<object> Players
        {
            get 
            {
                return _players ?? new ObservableCollection<object>();
            }
            set
            {
                _players = value;
                OnPropertyChanged("Players");
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
                PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.Name.Contains(_hostName) && x.Domain.Name == SelectedDomain));
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

        public ProblemViewModel()
        {
            _sshOptions = new SshOptions();

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
                PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.Domain.Name == _selectedDomain));
            }
        }


        public IEnumerable<string> DomainList
        {
            get
            {
                if (_domainList == null)
                    _domainList = CommonResources.Players.Select(x => x.Domain.Name).Distinct();
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
                var name = _selectedPlayer.Name.Substring(0, 14);
                if (Players.Contains(name))
                    return;
                else
                {
                    if (Players.Count() == 5)
                    {
                        MessageBox.Show("Limit of player to select is 5 please unselect players to select new ones");
                        return;
                    }

                    if (_checkHostNameIsValid(name))
                        Players.Add(new {Name = _selectedPlayer, Connected = IsConnected, Vnc = _sshOptions.GetVncPort() });
                }
            }
        }

        private void _scpConnection()
        {
            if (_checkHostNameIsValid())
            {

                _sshOptions.StartScpSession();
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
            Result = _sshOptions.ExecuteCommand(cmd);
        }

        private void _sshConnection()
        {
            //if (!_checkHostNameIsValid())
            //    return;

            ////try a ssh connection
            //_sshOptions.Host = HostName;
            _sshOptions.StartSshSession();
            IsConnected = _sshOptions._isConnected;
            if (IsConnected == true)
                Status = $"{HostName}. VNC Port 5999";



        }
        private bool _checkHostNameIsValid(string selected = "")
        {
        
            if (!Regex.IsMatch(selected.Trim(), "^[A-Za-z]{2}-[A-Za-z]{2}-[A-Za-z]{3}-[A-Za-z0-9]{4}$"))
            {
                MessageBox.Show("please enter a valid hostname e.g: UK-OT-LON-P001");
                return false;
            }
            //assign hostname to local host var in sshoptions
            _sshOptions.Host = selected;
            return true;
        }
    }
}
