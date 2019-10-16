﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentFTP;


namespace FTP_Download_SmartNet
{

    class TcFTPSite
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        

        public TcFTPSite(string host, string user, string password)
        {
            Host = host;
            User = user;
            Password = password;
        }



        public static List<string> GetFileList(FtpClient client, string host, DateTime start, DateTime end)
        {
            // pretty sure this is heinous, but I'm not sure why, or how to do it better
            List<string> hourLetter = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x" };

            // list to hold all files in URL
            List<string> fileList = new List<string>();

            int startHour = start.Hour;
            int endHour = end.Hour;

            if (startHour < endHour)
            {

                foreach (var item in client.GetNameListing(host))
                {
                    
                    var x = item.GetFtpFileName();

                    // list to hold file names to download
                    List<string> charList = new List<string>();
                    
                    for (int i = 0; i < hourLetter.Count; i++)
                    {
                        if (x[7].ToString().Contains(hourLetter[i]))
                        {
                            fileList.Add(item);
                        }
                    }
                    
                }

            }


            else
            {
                // code for change of URL for date change
                // process from start hour to midnight, then change URL and start from a until end hour
            }

            return fileList;
        }




        // takes listof file names and downloads to specified location.
        public static void FTPDownload(FtpClient client, string host, List<String> fileList, string folderLocation)
        {
            try
            {
                foreach (string item in client.GetNameListing(host))
                {
                    Console.WriteLine("Downloading: " + item.GetFtpFileName());
                    client.DownloadFile(folderLocation + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
