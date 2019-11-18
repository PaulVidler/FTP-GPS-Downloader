using System;
using System.Collections.Generic;

namespace FTP_Download_SmartNet
{
    public class BaseSpecs
    {
        public string BaseName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }


        // constructor
        public BaseSpecs(string basename, DateTime start, DateTime finish)
        {
            this.BaseName = basename;
            this.Start = start;
            this.Finish = finish;
        }

        public string StartUrl()
        {
            // /Hourly-1Sec/2019/09/04/ACL2
            string urlDirStr = $"/Hourly-1Sec/{this.Start.Year}/{this.Start.Month.ToString("00")}/{this.Start.Day.ToString("00")}/{this.BaseName}";
            return urlDirStr;
        }

        public string EndUrl()
        {
            // /Hourly-1Sec/2019/09/05/ACL2
            string urlEndDirStr = $"/Hourly-1Sec/{this.Finish.Year}/{this.Finish.Month.ToString("00")}/{this.Finish.Day.ToString("00")}/{this.BaseName}";
            return urlEndDirStr;
        }

        public string BaseList()
        {
            string baseListUrl = $"/Hourly-1Sec/{this.Start.Year}/{this.Start.Month.ToString("00")}/{this.Start.Day.ToString("00")}/";
            return baseListUrl;
        }


    }
}
