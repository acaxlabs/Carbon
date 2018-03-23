using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbon.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Net.Tests
{
    [TestClass()]
    public class ClientTests
    {
        //Use https://www.mocky to build request to test

        [TestMethod()]
        public void GetTest()
        {
            try
            {
                var res = new Client().Get("http://www.mocky.io/v2/5ab444412f00006400ca3b31");
                Assert.IsNotNull(res);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void PostTest()
        {
            try
            {
                var res = new Client().Post("http://www.mocky.io/v2/5ab444412f00006400ca3b31", new { message = "test" });
                Assert.IsNotNull(res);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void PutTest()
        {
            try
            {
                var res = new Client().Put("http://www.mocky.io/v2/5ab444412f00006400ca3b31", new { message = "test"});
                Assert.IsNotNull(res);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        
    }
}