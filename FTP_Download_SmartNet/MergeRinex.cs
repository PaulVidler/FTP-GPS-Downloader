using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace FTP_Download_SmartNet
{
    class MergeRinex
    {

        public List<string> FileList { get; set; }

        public BaseJobName JobName { get; set; }

        // constructer to pass in directory of unzipped files
        // public MergeRinex(){}

        public MergeRinex(BaseJobName jobName, List<string> rinexList)
        {
            JobName = jobName;
            FileList = rinexList;

        }


        // method to process rinex command to unzipped files
        public void MergeRinexTEQCCall()
        {
            string tecqCallG = "tecq";
            string tecqCallN = "tecq";
            string tecqCallO = "tecq";

            foreach (var item in FileList.Where(x=> x.EndsWith("g")))
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
