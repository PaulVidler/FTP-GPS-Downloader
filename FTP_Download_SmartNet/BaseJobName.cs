﻿using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace FTP_Download_SmartNet
{
    class BaseJobName
    {
        public String JobName { get; set; }
        // list of BaseSpecs for multibase downloads
        public List<BaseSpecs> BaseList { get; set; }
        public string JobDirectory { get; set; }
        public string LogFileString { get; set; }

        
        public BaseJobName(string jobDirectory, List<BaseSpecs> baseSpecs)
        {
            JobDirectory = jobDirectory;
            BaseList = baseSpecs;
        }
        
        
        public void UnzipFiles()
        {
            // code to unzip downloaded files
            ZipFile.ExtractToDirectory($"{JobDirectory}/*.zip", JobDirectory);
        }

        public void MergeFiles(string unzippedFileLocation)
        {
            // code to call TEQC and merge base data
        }
    }
}
