using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TflRoadStatusApp.Helpers;
using TflRoadStatusApp.Helpers.Interfaces;
using TflRoadStatusApp.Modal.enums;

namespace TflRoadStatusApp.Test
{
    [TestClass]
    public class TflRoadAppMainTest
    {
        TflRoadAppMain mainApp;
        Mock<ITflRoadMessageHelper> messageHelperMock;
        ILogger<TflRoadAppMain> loggerMock;

        [TestInitialize]
        public void TflRoadAppMainTest_Init()
        {
            loggerMock = Mock.Of<ILogger<TflRoadAppMain>>();
            messageHelperMock = new Mock<ITflRoadMessageHelper>();
            messageHelperMock.Setup(x => x.Print(It.IsAny<string>()));
            messageHelperMock.Setup(x => x.Read()).Returns("");
            messageHelperMock.Setup(x => x.PrintSuccessMessage(It.IsAny<string>())).Returns((int)ExitCode.Success);
            messageHelperMock.Setup(x => x.PrintInvalidRoadMessage(It.IsAny<string>())).Returns((int)ExitCode.InvalidRoad);
        }
        [TestMethod]
        public void Test_RunAsync_ForSuccessResponse()
        {
            var svcMock = new Mock<ITflRoadService>();
            svcMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage()
                    { Content = new StringContent("[{}]"), StatusCode = System.Net.HttpStatusCode.OK }));

            mainApp = new TflRoadAppMain(svcMock.Object, loggerMock, messageHelperMock.Object);
            var result = mainApp.RunAsync(Array.Empty<string>()).Result;
            Assert.AreEqual((int)ExitCode.Success, result);
        }

        [TestMethod]
        public void Test_RunAsync_ForInvalidResponse()
        {
           
            var svcMock = new Mock<ITflRoadService>();
            svcMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage()
                { Content = new StringContent("[{}]"), StatusCode = System.Net.HttpStatusCode.NotFound }));

            mainApp = new TflRoadAppMain(svcMock.Object, loggerMock, messageHelperMock.Object);
            var result = mainApp.RunAsync(Array.Empty<string>()).Result;
            Assert.AreEqual((int)ExitCode.InvalidRoad, result);
        }

        [TestMethod]
        public void Test_RunAsync_ForExceptionCheck()
        {
            var svcMock = new Mock<ITflRoadService>();
            svcMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(()=>null);

            mainApp = new TflRoadAppMain(svcMock.Object, loggerMock, messageHelperMock.Object);
            var result = mainApp.RunAsync(Array.Empty<string>()).Result;
            Assert.AreEqual((int)ExitCode.UnknownError, result);
        }

    }
}
