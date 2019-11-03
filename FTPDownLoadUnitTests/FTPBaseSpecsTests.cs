using FTP_Download_SmartNet;
using System;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class FTPBaseSpecsTests
    {
        private readonly string testBase = "test";
        private readonly DateTime testStart = DateTime.UtcNow;
        private readonly DateTime testEnd = DateTime.Now;


        [Fact]
        public void BaseSpecsConstructorTest()
        {
            var baseTest = new BaseSpecs(testBase, testStart, testEnd);
            Assert.Equal(testBase, baseTest.BaseName);
            Assert.Equal(testStart, baseTest.Start);
            Assert.Equal(testEnd, baseTest.Finish);
            
        }

        [Fact]
        public void StartURLMethodTest()
        {
            var baseTest = new BaseSpecs(testBase, testStart, testEnd);
            Assert.Equal($"/Hourly-1Sec/{testStart.Year}/0{testStart.Month}/0{testStart.Day}/{testBase}", baseTest.StartUrl());

        }

        [Fact]
        public void EndURLMethodTest()
        {
            var baseTest = new BaseSpecs(testBase, testStart, testEnd);
            Assert.Equal($"/Hourly-1Sec/{testEnd.Year}/0{testEnd.Month}/0{testEnd.Day}/{testBase}", baseTest.EndUrl());

        }

    }
}
