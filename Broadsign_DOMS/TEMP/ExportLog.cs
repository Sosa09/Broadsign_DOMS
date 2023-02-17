using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.TEMP
{
    class ExportLog
    {
        string playerName = "ES-MA-VAG-P072";
        string remotePath = "/opt/broadsign/suite/bsp/share/bsp/bsp.log";
        string wslPath = @"\\wsl.localhost\Ubuntu\home\becco1sar\Documents\";
        string destinationPath = "/home/becco1sar/Documents/";
        string finalPath = @"C:\users\becco1sar\";
        //string succeed = "File not copied to " + finalPath;




        //_copyFileFromRemoteToLocal(remotePath, destinationPath, playerName);



    //        if (_fileCopiedFromRemoteToLocal(wslPath + playerName + ".log"))
    //        {

    //            if (File.Exists(finalPath + playerName + ".log"))
    //            {
    //                File.Delete(finalPath + playerName + ".log");
    //            }

    //File.Copy(wslPath + playerName + ".log", finalPath + playerName + ".log");
    //            succeed = "File copied to " + finalPath;
            



            

        

        static bool _wslAvailable(string wslPath)
        {
            bool wslIsAvailable = false;

            if (Directory.Exists(wslPath))
            {
                wslIsAvailable = true;
            }

            return wslIsAvailable;
        }

        static bool _fileCopiedFromRemoteToLocal(string finalDestinationFilePath)
        {
            bool copyDone = false;
            if (File.Exists(finalDestinationFilePath))
            {
                copyDone = true;
            }
            return copyDone;
        }

        private void _copyFileFromRemoteToLocal(string sourcePath, string tempDestinationPath, string player)
        {
            string wslCmd = $"scp -J ubuntu@wireguard.ccuk.io ccplayer@{player}:{sourcePath} {tempDestinationPath}";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "wsl";
            startInfo.Arguments = "-e bash -c \"" + wslCmd + "\"";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();


            // Read output from ssh command.
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);

            process.WaitForExit();
        }

        private void _openFile(string path)
        {
            File.Open(path, FileMode.Open);
        }
    }
}
