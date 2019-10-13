using FluentFTP;
using System;
using System.Net;

namespace FTP_Download_SmartNet
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = new DateTime(2019, 09, 04, 23, 0, 0);
            DateTime endTime = new DateTime(2019, 09, 05, 3, 0, 0);
            string baseName = "7CRA";

            BaseSpecs jobBase = new BaseSpecs(baseName, startTime, endTime);

            string hostUri = 
            string host = jobBase.StartUrl();
            string user = 
            string pass = 

            FTP_Site smartnet = new FTP_Site(hostUri, user, pass);



            FtpClient client = new FtpClient(smartnet.Host);

            client.Credentials = new NetworkCredential(smartnet.User, smartnet.Password);

            client.Connect();

            //foreach (FtpListItem item in client.GetListing(host))
            foreach (string item in client.GetNameListing(host))
            {
                try
                {
                    Console.WriteLine(item.GetFtpFileName());
                    //client.DownloadFile(@"D:\Testing-DIR-for-Code\" + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());
                    client.DownloadFile(@"D:\Testing-DIR-for-Code\david.zip", host + @"\" + item.GetFtpFileName());
                }
                catch (Exception)
                {
                    Console.WriteLine(item + " not found");
                    
                }
                continue;
            }



            // Start URL works
            // needs error checking for empty values or errors etc
            // maybe look at unit testing as an excercise?
            // Need to work out exactly what to download, based on times of day. Maybe filter on "if last four chars = 'x'" or whatever.
            // need to be able to error check and report on time gaps etc
            // 

        }
    }
}
