using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;

namespace FTP_Download_SmartNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // year/month/day/hour/minute/second
            DateTime startTime = new DateTime(2019, 11, 8, 1, 0, 0);
            DateTime endTime = new DateTime(2019, 11, 8, 4, 0, 0);
            string baseName = "4CRY";

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

            // List<string> testBaseList = smartnet.GetBaseList(client, startTime);

            TcFTPSite.FTPDownload(client, host, hostEnd, fileList, Environment.CurrentDirectory);

            

        }
    }
}
