using Microsoft.Win32;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;



namespace Broadsign_DOMS.Service
{
    public class SshOptions : ObservableObject
    {
        string _jumpHost = "wireguard.ccuk.io";
        string _jumpUsername = "ubuntu";
        string _username = "ccplayer";
        string _password = "test1234";
        bool _isConnected;
        string _hostName;

        PrivateKeyFile privateKeyFile;
        ConnectionInfo _connectionInfo;
        ForwardedPortLocal _forwardedPortLocal;
        ForwardedPortLocal _forwardedPortLocalScp;
        ForwardedPortLocal _forwardedPortLocalVnc;
        ScpClient scpClient;
        SshClient sshClient;
        SshClient sshJump;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }
        public int VncPort { get; set; }
        public string HostName { get; set; }

        int[] port = new int[] { 5999, 6000, 6001, 6002, 6003, 6004, 6005, 6006, 6007, 6008, 6009, 6010, 60200, 60201, 60202 };
        int index = 0;

        


        public SshOptions()
        {
            //TODO: Make it selectable so that the user can establish a valid connectoin
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string sshKey = userProfile + "\\.ssh\\id_rsa";

            _connectSshJump(sshKey);
       
        }
        private void _connectSshJump(string sshKey = "")
        {
            if(sshKey == "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                sshKey = openFileDialog.FileName;
            }
            try
            {
                privateKeyFile = new PrivateKeyFile(sshKey);
                _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));
                sshJump = new SshClient(_connectionInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
    
            }
        }
        public void StartSshSession()
        {
  
            if(sshJump == null)
            {
                MessageBox.Show("ssh key was not found, select an valid ssh key to continue");
                _connectSshJump();
                StartSshSession();

            }
            try
            {

                if(!sshJump.IsConnected)
                    sshJump.Connect();

                _forwardedPortLocal     = new ForwardedPortLocal("localhost", HostName, 22);
                
                sshJump.AddForwardedPort(_forwardedPortLocal);
                
                _forwardedPortLocal.Start();

                _connectionInfo = new ConnectionInfo(_forwardedPortLocal.BoundHost,(int)_forwardedPortLocal.BoundPort,_username, new PasswordAuthenticationMethod(_username, _password));
                try
                {
                    sshClient = new SshClient(_connectionInfo);
                    sshClient.Connect();               
                    IsConnected = true;
                    _startVncSession();
             

                }
                catch (Exception e)
                {
                   MessageBox.Show(e.Message);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }


        }
        public void DisconnectSshSession()
        {
            sshClient.Disconnect();
            _forwardedPortLocal.Stop();
            _forwardedPortLocalVnc.Stop();

            sshJump.Disconnect();
        }
        public string ExecuteCommand(string cmd)
        {
            var result = "";
            if (!sshClient.IsConnected)
                StartSshSession();
            try
            {
                result = sshClient.CreateCommand($"{cmd}").Execute();                                           
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _forwardedPortLocal.Dispose();
            }

            return result;
        }
        private void _startScpSession()
        {
            // Create an SSH client for the jump host
            var jumpHostConnectionInfo = new ConnectionInfo("wireguard.ccuk.io", 22, "ubuntu", new PrivateKeyAuthenticationMethod("ubuntu", privateKeyFile));
            using var jumpHostClient = new SshClient(jumpHostConnectionInfo);

            // Connect to the jump host
            jumpHostClient.Connect();

            //Create a forwarded port tunnel on the jump host


           _forwardedPortLocalScp = new ForwardedPortLocal("localhost", HostName, 22);
            sshJump.AddForwardedPort(_forwardedPortLocalScp);
            _forwardedPortLocalScp.Start();

            // Create an SSH client for the remote host
            var remoteHostConnectionInfo = new ConnectionInfo("localhost", (int)_forwardedPortLocalScp.BoundPort, _username, new PasswordAuthenticationMethod(_username, _password));
            //sshClient = new SshClient(remoteHostConnectionInfo);

            //// Connect to the remote host using the forwarded port
            //sshClient.Connect();
            
            // SCP client for transferring files
            scpClient = new ScpClient(remoteHostConnectionInfo);
        
            // Connect SCP client
            scpClient.Connect();
        
            
        }
        private void _startVncSession()
        {
  
            var _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));

            try
            {
                var sshJump = new SshClient(_connectionInfo);
                sshJump.Connect();
                //return jump connection true


                _forwardedPortLocalVnc = new ForwardedPortLocal("localhost", (uint)port[index], HostName, 5900);
                sshJump.AddForwardedPort(_forwardedPortLocalVnc);
                _forwardedPortLocalVnc.Start();
                VncPort = (int)_forwardedPortLocalVnc.BoundPort;

            }
            catch (Exception e)
            {
                //problem with jump connection
                Debug.WriteLine(e.Message);
                index++;
                _startVncSession();
                
            }
        }
        public ObservableCollection<string> GetLogList()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>
            {
                "/opt/broadsign/suite/bsp/share/bsp/bsp.log",
                "/opt/broadsign/suite/bsp/share/bsp/bsp.db"
            };
            string[] cmd = ExecuteCommand("ls /opt/broadsign/suite/bsp/share/bsp/logs/").Split('\n');
            foreach(string line in cmd)
            {
                collection.Add($"/opt/broadsign/suite/bsp/share/bsp/logs/{line}");

            }
            return collection;
        }
        public ObservableCollection<string> GetAdCopies()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();
            
            string[] cmd = ExecuteCommand("sudo ls /opt/broadsign/suite/bsp/share/documents/").Split('\n');

            foreach (string line in cmd)
            {
                collection.Add($"/opt/broadsign/suite/bsp/share/documents/{line}");
            }
            return collection;
        }
        public void DownloadFiles(ObservableCollection<string> files)
        {
            //TODO: Add a boolean to see if the files a downloadable or not only log files should be !
            _startScpSession();
            var localFilePath = "";
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    localFilePath = dialog.SelectedPath;
                }
            }
            foreach(string file in files)
            {
                // Download a remote file
                string remoteFilePath = file;


                string fileName = $"{localFilePath}\\{file.Substring(file.LastIndexOf('/'))}";

                var fileStream = new FileStream(fileName, FileMode.Create);

                try
                {
                    scpClient.Download(remoteFilePath, fileStream);


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }

    }
}
