﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Threading;
using FluentFTP;
using System.Diagnostics;

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

        public List<string> GetBaseList(FtpClient client, DateTime start)
        {
            List<string> baseList = new List<string>();
            string urlDirStr =$"/Hourly-1Sec/{start.Year}/{start.Month.ToString("00")}/{start.Day.ToString("00")}";
            var listing = client.GetNameListing(urlDirStr);

            foreach (var item in listing)
            {
                baseList.Add(item.GetFtpFileName());
            }
            
            return baseList; 
        }


        public static List<string> GetFileList(FtpClient client, string host, string hostEnd, DateTime start, DateTime end)
        {
            // pretty sure this is heinous, but I'm not sure why, or how to do it better
            List<string> hourLetter = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x" };

            // list to hold all files in URL
            List<string> fileList = new List<string>();

            int startHour = start.Hour;
            int endHour = end.Hour;

            // if hours do not cross into next day
            if (startHour < endHour)
            {
                List<string> hourList = new List<string>();

                // add letters to hourListbased on startHour and endHour
                for (int i = startHour; i < endHour; i++)
                {
                    hourList.Add(hourLetter[i]);
                }


                // iterate over list of file names in target host connection
                foreach (var item in client.GetNameListing(host))
                {
                    
                    var x = item.GetFtpFileName();

                    for (int i = 0; i < hourList.Count; i++)
                    {
                        if (x[7].ToString() == hourList[i])
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
                List<string> hourList = new List<string>();
                List<string> endHourList = new List<string>();
                string[] listing = client.GetNameListing(host);
                string[] listingEnd = client.GetNameListing(hostEnd);

                // add letters to hourListbased on startHour and endHour
                for (int i = startHour; i < hourLetter.Count; i++)
                {
                    hourList.Add(hourLetter[i]);
                }

                // add hours from startHour (?) to midnight ('x')
                foreach (var item in listing)
                {
                    var x = item.GetFtpFileName();

                    for (int i = 0; i < hourList.Count; i++)
                    {
                        if (x[7].ToString() == hourList[i])
                        {
                            fileList.Add(item);
                        }
                    }
                }

                // same code as above, but takes into account the new host URL for data change
                // add letters to hourListbased on startHour and endHour
                for (int i = 0; i <= endHour; i++)
                {
                    endHourList.Add(hourLetter[i]);
                }

                // add hours from startHour (?) to midnight ('x')
                foreach (var item in listingEnd)
                {
                    var x = item.GetFtpFileName();

                    for (int i = 0; i < endHourList.Count; i++)
                    {
                        if (x[7].ToString() == endHourList[i])
                        {
                            fileList.Add(item);
                        }
                    }

                }

            }

            return fileList;
        }



        // takes list of file names and downloads to specified location.
        // *********** NEEDS TO BE MADE ASYNC!!!!!!! *****************
        public static void FTPDownload(FtpClient client, string host, string hostEnd , List<String> fileList, string folderLocation)
        {
            int fileListCounter = 0;
            int fileListCounter2 = 0;

            // loop over first list (List for first day)
            foreach (string item in client.GetNameListing(host))
            {
                if (fileListCounter < fileList.Count)
                {

                    if (fileList[fileListCounter].Contains(item.GetFtpFileName()))
                    {
                        Console.WriteLine("Downloading: " + item.GetFtpFileName());
                        client.DownloadFile(folderLocation + @"\" + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());

                        fileListCounter++;

                    }
                }
                
            }

            // loop over second list for second day
            foreach (string item2 in client.GetNameListing(hostEnd))
            {
                if (fileListCounter < fileList.Count)
                {

                    if (fileList[fileListCounter].Contains(item2.GetFtpFileName()))
                    {
                        Console.WriteLine("Downloading: " + item2.GetFtpFileName());
                        client.DownloadFile(folderLocation + @"\" + item2.GetFtpFileName(), hostEnd + @"\" + item2.GetFtpFileName());

                        fileListCounter++;

                    }

                }

            }

            // cant have 2 x same file name from the same base, even if it's on a different day

            // wait for downloads to finish. Can probably be removed once async is implemented
            Thread.Sleep(10000);

            string[] filePaths = Directory.GetFiles(folderLocation, "*.zip");

            foreach (var item in filePaths)
            {
                ZipFile.ExtractToDirectory(item.ToString(), folderLocation);
            }

        }

        public static void MergeRinexTEQCCall()
        {
            string tecqCallG = "tecq";
            string tecqCallN = "tecq";
            string tecqCallO = "tecq";

            string JobName = "testJob";

            string[] FileList = Path.GetFileName(Environment.CurrentDirectory);


            foreach (var item in FileList.Where(x => x.EndsWith("g")))
            {
                tecqCallG = tecqCallG + " " + item;
            }

            tecqCallG = tecqCallG + " > " + JobName + ".19g";

            foreach (var item in FileList.Where(x => x.EndsWith("n")))
            {
                tecqCallN = tecqCallN + " " + item;
            }

            tecqCallN = tecqCallN + " > " + JobName + ".19n";

            foreach (var item in FileList.Where(x => x.EndsWith("o")))
            {
                tecqCallO = tecqCallO + " " + item;
            }

            tecqCallO = tecqCallO + " > " + JobName + ".19o";

            Process.Start($"{tecqCallG}");
            Process.Start($"{tecqCallN}");
            Process.Start($"{tecqCallO}");

        }


    }
}
