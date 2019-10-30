using System;
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



        // takes listof file names and downloads to specified location.

        public static void FTPDownload(FtpClient client, string host, string hostEnd , List<String> fileList, string folderLocation)
        {
            try
            {
                foreach (string item in client.GetNameListing(host))
                {
                    int fileListCounter = 0;

                    if (fileList[fileListCounter].Contains(item.GetFtpFileName()))
                    {
                        Console.WriteLine("Downloading: " + item.GetFtpFileName());
                        client.DownloadFile(folderLocation + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());

                        fileListCounter++;
                    }

                }

                foreach (string item in client.GetNameListing(hostEnd))
                {
                    int fileListCounter = 0;

                    if (fileList[fileListCounter].Contains(item.GetFtpFileName()))
                    {
                        Console.WriteLine("Downloading: " + item.GetFtpFileName());
                        client.DownloadFile(folderLocation + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());

                        fileListCounter++;
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }

        }



        //public static void FTPDownload(FtpClient client, string host, List<String> fileList, string folderLocation)
        //{
        //    try
        //    {
        //        foreach (string item in client.GetNameListing(host))
        //        {
        //            Console.WriteLine("Downloading: " + item.GetFtpFileName());
        //            client.DownloadFile(folderLocation + item.GetFtpFileName(), host + @"\" + item.GetFtpFileName());

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

    }
}
