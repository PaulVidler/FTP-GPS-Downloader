using System;

namespace FTP_Download_SmartNet
{
    class BaseSpecs
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
            string urlDirStr = $"/Hourly-1Sec/{this.Start.Year}/0{this.Start.Month}/0{this.Start.Day}/{this.BaseName}";

            return urlDirStr;
        }

        public string EndUrl()
        {
            // /Hourly-1Sec/2019/09/05/ACL2
            string urlEndDirStr = $"/Hourly-1Sec/{this.Finish.Year}/{this.Finish.Month}/{this.Finish.Day}/{this.BaseName}";

            return urlEndDirStr;
        }

    }
}
