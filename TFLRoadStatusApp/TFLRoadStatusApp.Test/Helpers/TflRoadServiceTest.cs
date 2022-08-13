using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TflRoadStatusApp.Helpers;

namespace TflRoadStatusApp.Test.Helpers
{
    [TestClass]
    public class TflRoadServiceTest
    {        

        [TestMethod]
        public async Task TestGet()
        {
            const string testContent = "Response 200 From Server";          

            Mock<IConfiguration> config = new Mock<IConfiguration>(); 
            config.SetupGet(x => x[It.Is<string>(s => s == "app_key")]).Returns("");
            config.SetupGet(x => x[It.Is<string>(s => s == "app_id")]).Returns("");

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });
            var svc = new TflRoadService(new HttpClient(mockMessageHandler.Object), config.Object);
            var response = await (await svc.Get("http://test.com")).Content.ReadAsStringAsync();
            Assert.AreEqual(response.Length, testContent.Length);

        }
    }
}
 
