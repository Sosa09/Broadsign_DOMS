using Microsoft.Win32;
using Renci.SshNet;
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
            try
            {
                //TODO: Make it selectable so that the user can establish a valid connectoin
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string sshKey = userProfile + "\\.ssh\\id_rsa";
                privateKeyFile = new PrivateKeyFile(sshKey);
                _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));
                sshJump = new SshClient(_connectionInfo);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
       
        }
        public void StartSshSession()
        {
  
            
            try
            {
                if(!sshJump.IsConnected)
                    sshJump.Connect();
                _forwardedPortLocal     = new ForwardedPortLocal("localhost", HostName, 22);
                _forwardedPortLocalScp  = new ForwardedPortLocal("localhost", HostName, 54022);
                _forwardedPortLocalVnc  = new ForwardedPortLocal("localhost", (uint)port[index], HostName, 5900);
                
                sshJump.AddForwardedPort(_forwardedPortLocalVnc);
                sshJump.AddForwardedPort(_forwardedPortLocal);
                sshJump.AddForwardedPort(_forwardedPortLocalScp);

                _forwardedPortLocalVnc.Start();
                _forwardedPortLocal.Start();

                _connectionInfo = new ConnectionInfo(_forwardedPortLocal.BoundHost,(int)_forwardedPortLocal.BoundPort,_username, new PasswordAuthenticationMethod(_username, _password));
                try
                {
                    sshClient = new SshClient(_connectionInfo);
                    sshClient.Connect();               
                    IsConnected = true;
                    _startVncSession();
                    _startScpSession();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }


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
            //// Create an SSH client for the jump host
            //var jumpHostConnectionInfo = new ConnectionInfo("wireguard.ccuk.io", 22, "ubuntu", new PrivateKeyAuthenticationMethod("ubuntu", privateKeyFile));
            //using var jumpHostClient = new SshClient(jumpHostConnectionInfo);

            //// Connect to the jump host
            //jumpHostClient.Connect();

            // Create a forwarded port tunnel on the jump host


            //_forwardedPortLocalScp = new ForwardedPortLocal("localhost", HostName, 22);
            //sshJump.AddForwardedPort(_forwardedPortLocalScp);
            //_forwardedPortLocalScp.Start();

            // Create an SSH client for the remote host
            var remoteHostConnectionInfo = new ConnectionInfo("localhost", (int)_forwardedPortLocal.BoundPort, _username, new PasswordAuthenticationMethod(_username, _password));
            sshClient = new SshClient(remoteHostConnectionInfo);

            // Connect to the remote host using the forwarded port
            sshClient.Connect();
            
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
                //var sshJump = new SshClient(_connectionInfo);
                //sshJump.Connect();
                //return jump connection true
                
                //_forwardedPortLocalVnc = new ForwardedPortLocal("localhost", (uint)port[index], HostName, 5900);
                //sshJump.AddForwardedPort(_forwardedPortLocalVnc);
                //_forwardedPortLocalVnc.Start();
                VncPort = (int)_forwardedPortLocalVnc.BoundPort;

            }
            catch (Exception e)
            {
                //problem with jump connection
                MessageBox.Show(e.Message);
                index++;
                _startVncSession();
                
            }
        }
        public ObservableCollection<string> GetLogList()
        {
            var collection = new ObservableCollection<string>
            {
                "bsp.log",
                "chromium.log",
                "bsp.db"

            };
            StreamReader streamReader = new StreamReader(ExecuteCommand("ls /opt/broadsign/suite/bsp/share/bsp/logs/"));
            while (!streamReader.EndOfStream)
                collection.Add(streamReader.ReadLine());
            return collection;
        }
        public void DownloadFiles(ObservableCollection<string> files)
        {
            var localFilePath = "";
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    localFilePath = dialog.SelectedPath;
                }
            }
            foreach (var file in files)
            {
                // Download a remote file
                string remoteFilePath = "/opt/broadsign/suite/bsp/share/bsp/";
                if (file != "bsp.log" || file != "bsp.db")
                    remoteFilePath = remoteFilePath + "logs/" + file;
                else
                    remoteFilePath = remoteFilePath + file;


                using (var fileStream = new FileStream(localFilePath + $"{HostName}-bsp.log", FileMode.Create))
                {
                    scpClient.Download(file, fileStream);
                }

             
            }


            // Disconnect and clean up
            scpClient.Disconnect();
            _forwardedPortLocalScp.Stop();
        }
    }
}
