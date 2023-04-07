using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class ProblemViewModel : ObservableObject, IPageViewModel 
    {
        private ICommand _remoteOptionsCommand;
        private ICommand _connectSshCommand;
        private string _hostName;
        private SshOptions _sshOptions;
        private bool _isConnected;
        private string _status;
        private string _result;
        private ICommand _connectScpCommand;

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
                    )) ;
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
            if (!_checkHostNameIsValid())
                return;
            
            //try a ssh connection
            _sshOptions.Host = HostName;
            _sshOptions.StartSshSession();
            IsConnected = _sshOptions._isConnected;
            if (IsConnected == true)
                Status = $"{HostName}. VNC Port 5999";
                 
            

        }
        private bool _checkHostNameIsValid()
        {
            if (HostName != null || string.Empty != HostName)
            {

                if(!Regex.IsMatch(HostName.Trim(), "^[A-Za-z]{2}-[A-Za-z]{2}-[A-Za-z]{3}-[A-Za-z0-9]{4}$"))
                {
                    MessageBox.Show("please enter a valid hostname e.g: UK-OT-LON-P001");
                    return false;
                }
                    

            }
            else
            {
                MessageBox.Show("Please select at least one player ");
                return false;
            }
            //assign hostname to local host var in sshoptions
            _sshOptions.Host = HostName;
            return true;
        }
    }
}
