using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FTP_Download_SmartNet
{
    class MergeRinex
    {

        public List<string> GList { get; set; }
        public List<string> NList { get; set; }
        public List<string> OList { get; set; }

        public BaseJobName JobName { get; set; }

        // constructer to pass in directory of unzipped files
        // public MergeRinex(){}


        // method to process rinex command to unzipped files
        public static void MergeRinexTEQCCall(List<string> rinexList)
        {
            string tecqCallG = "tecq";
            string tecqCallN = "tecq";
            string tecqCallO = "tecq";

            foreach (var item in rinexList.Where(x=> x.EndsWith("g")))
            {
                tecqCallG = tecqCallG + " " + item;
            }

            tecqCallG = tecqCallG + ""
        }

        



    }
}
