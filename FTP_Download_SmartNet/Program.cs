using FluentFTP;
using System;
using System.Net;

namespace FTP_Download_SmartNet
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = new DateTime(2019, 08, 07, 17, 0, 0);
            DateTime endTime = new DateTime(2019, 09, 07, 2, 0, 0);
            string baseName = "WYRL";

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

            // Start URL works
            // needs error checking for empty values or errors etc
            // maybe look at unit testing as an excercise?
            // Need to work out exactly what to download, based on times of day. Maybe filter on "if last four chars = 'x'" or whatever.
            // need to be able to error check and report on time gaps etc
            
            
            
            // if download fails due to month change or naming issues. Maybe try to download everything in the file as a backup so as not to throw an exception 

        }
    }
}
