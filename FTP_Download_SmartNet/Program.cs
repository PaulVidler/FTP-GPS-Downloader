using FluentFTP;
using System;
using System.Net;

namespace FTP_Download_SmartNet
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = new DateTime(2019, 07, 09, 11, 0, 0);
            DateTime endTime = new DateTime(2019, 07, 09, 14, 0, 0);
            string baseName = "MCHY";

            BaseSpecs jobBase = new BaseSpecs(baseName, startTime, endTime);

            string hostUri = "smartnetaus.com";
            string host = jobBase.StartUrl();
            string hostEnd = jobBase.EndUrl();
            string user = "aerometrex";
            string pass = "aerometrex";

            TcFTPSite smartnet = new TcFTPSite(hostUri, user, pass);
            FtpClient client = new FtpClient(smartnet.Host);

            client.Credentials = new NetworkCredential(smartnet.User, smartnet.Password);
            client.Connect();

            var fileList = TcFTPSite.GetFileList(client, host, hostEnd, startTime, endTime);

            TcFTPSite.FTPDownload(client, host, hostEnd, fileList, Environment.CurrentDirectory);

            
            // need to be able to error check and report on time gaps etc
            // if download fails due to month change or naming issues. Maybe try to download everything in the file as a backup so as not to throw an exception 

        }
    }
}
