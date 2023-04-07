using Microsoft.Win32;
using Renci.SshNet;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;


namespace Broadsign_DOMS.Service
{
    public class SshOptions
    {
        public string Host;
        private string _jumpHost = "wireguard.ccuk.io";
        private string _jumpUsername = "ubuntu";
        private string _username = "ccplayer";
        private string _password = "test1234";
        private PrivateKeyFile privateKeyFile = new PrivateKeyFile("C:\\Users\\BECCO1SAR\\.ssh\\id_rsa");
        private ConnectionInfo _connectionInfo;
        private ForwardedPortLocal _forwardedPortLocal;
        ScpClient scpClient;
        SshClient sshClient;
        SshClient sshJump;
        public bool _isConnected;

        public SshOptions(string host)
        {
            Host = host;
        }
        public SshOptions()
        {
            _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));
            sshJump = new SshClient(_connectionInfo);
        }
        public void StartSshSession()
        {
  
            
            try
            {
                if(!sshJump.IsConnected)
                    sshJump.Connect();
                _forwardedPortLocal = new ForwardedPortLocal("localhost", Host, 22);
                sshJump.AddForwardedPort(_forwardedPortLocal);
                _forwardedPortLocal.Start();

                _connectionInfo = new ConnectionInfo(_forwardedPortLocal.BoundHost,(int)_forwardedPortLocal.BoundPort,_username, new PasswordAuthenticationMethod(_username, _password));
                try
                {
                    sshClient = new SshClient(_connectionInfo);
                    sshClient.Connect();
                    MessageBox.Show("connected ! opening VNC");
                    _isConnected = true;
                    StartVncSession();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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




        public void StartScpSession()
        {
            //// Create an SSH client for the jump host
            //var jumpHostConnectionInfo = new ConnectionInfo("wireguard.ccuk.io", 22, "ubuntu", new PrivateKeyAuthenticationMethod("ubuntu", privateKeyFile));
            //using var jumpHostClient = new SshClient(jumpHostConnectionInfo);

            //// Connect to the jump host
            //jumpHostClient.Connect();

            // Create a forwarded port tunnel on the jump host

            _forwardedPortLocal = new ForwardedPortLocal("localhost", Host, 22);
            sshJump.AddForwardedPort(_forwardedPortLocal);
            _forwardedPortLocal.Start();

            // Create an SSH client for the remote host
            var remoteHostConnectionInfo = new ConnectionInfo("localhost", (int)_forwardedPortLocal.BoundPort, _username, new PasswordAuthenticationMethod(_username, _password));
            using var remoteHostClient = new SshClient(remoteHostConnectionInfo);

            // Connect to the remote host using the forwarded port
            remoteHostClient.Connect();

            // SCP client for transferring files
            scpClient = new ScpClient(remoteHostConnectionInfo);

            // Connect SCP client
            scpClient.Connect();

            // Download a remote file
            string remoteFilePath = "/opt/broadsign/suite/bsp/share/bsp/bsp.log";
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var localFilePath = fileDialog.FileName;
            
            using (var fileStream = new FileStream(localFilePath + $"{Host}-bsp.log", FileMode.Create))
            {
                scpClient.Download(remoteFilePath, fileStream);
            }

            Console.WriteLine("File downloaded successfully!");

            // Disconnect and clean up
            scpClient.Disconnect();
            remoteHostClient.Disconnect();
            _forwardedPortLocal.Stop();
        }

        public void StartVncSession()
        {
  
            var _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));
            try
            {
                var sshJump = new SshClient(_connectionInfo);
                sshJump.Connect();
                //return jump connection true
                var _forwardedPortLocal = new ForwardedPortLocal("localhost", 5999, Host, 5900);
                sshJump.AddForwardedPort(_forwardedPortLocal);
                _forwardedPortLocal.Start();

            }
            catch (Exception e)
            {
                //problem with jump connection
                MessageBox.Show(e.Message);
            }
        }


    }
}
