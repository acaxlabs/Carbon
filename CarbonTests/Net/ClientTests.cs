using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbon.Net;

namespace CarbonTests.Net
{
	[TestClass]
    public class ClientTests
    {
        //Use https://www.mocky to build request to test

        [TestMethod]
		[TestCategory("Smoke")]
        public void GetSendsValidGetRequest()
        {
			var res = new Client().Get("http://httpbin.org/get");
			Assert.IsNotNull(res);
        }

        [TestMethod]
		[TestCategory("Smoke")]
        public void PostSendsValidPostRequest()
        {
			var res = new Client().Post("http://httpbin.org/post", new { message = "test" });
			Assert.IsNotNull(res);
		}

		[TestMethod]
		[TestCategory("Smoke")]
        public void PutSendsValidPutRequest()
        {
			var res = new Client().Put("http://httpbin.org/put", new { message = "test" });
			Assert.IsNotNull(res);
		}
	}
}