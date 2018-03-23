using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbon.Net;

namespace CarbonTests.Net
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        [TestCategory("Smoke")]
        [TestCategory("Get")]
        public void GetSendsValidGetRequest()
        {
            var res = new Client().Get("http://httpbin.org/get");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestCategory("Post")]
        public void PostSendsValidPostRequest()
        {
            var res = new Client().Post("http://httpbin.org/post", new { message = "test" });
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestCategory("Put")]
        public void PutSendsValidPutRequest()
        {
            var res = new Client().Put("http://httpbin.org/put", new { message = "test" });
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("Post")]
        public void PostSendsDataInRequest()
        {
            string postTestString = "ThisIsThePostTestStringImLookingFor";
            var res = new Client().Post("http://httpbin.org/post", new { message = postTestString });
            Assert.IsTrue(res.Contains(postTestString), "Expected string was not found in response");
        }

        [TestMethod]
        [TestCategory("Put")]
        public void PutSendsDataInRequest()
        {
            string putTestString = "ThisIsThePutTestStringImLookingFor";
            var res = new Client().Put("http://httpbin.org/put", new { message = putTestString });
            Assert.IsTrue(res.Contains(putTestString), "Expected string was not found in response");
        }
    }
}