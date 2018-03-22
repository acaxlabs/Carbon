using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbon.Ioc;

namespace CarbonTests.Ioc
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void TestConfig()
        {
            IExample e = Container.Get<IExample>();
            Assert.IsNotNull(e);
        }
    }
}
