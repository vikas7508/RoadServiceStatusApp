using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TflRoadStatusApp.Helpers;
using TflRoadStatusApp.Modal.enums;

namespace TflRoadStatusApp.Test.Helpers
{
    [TestClass]
    public class TflRoadMessageHelperTest
    {         
        TflRoadMessageHelper printSvc;
        [TestInitialize]
        public void TflRoadMessageHelperTest_Init()
        { 
            printSvc = new TflRoadMessageHelper();
        }       

        [TestMethod]
        public void Test_PrintInvalidRoadMessage()
        {
            int returnValue = printSvc.PrintInvalidRoadMessage("{}"); 
            Assert.AreEqual(returnValue, (int)ExitCode.InvalidRoad);
        }

        [TestMethod]
        public void Test_PrintSuccessMessage()
        {
            int returnValue = printSvc.PrintSuccessMessage("[{}]");             
            Assert.AreEqual(returnValue, (int)ExitCode.Success);
        }
    }

    
}
