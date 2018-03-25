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

        [TestMethod]
        public void TestConfig2()
        {
            // do this a second time to ensure that the instantiator works 
            // without regstering the class again. 
            IExample e = Container.Get<IExample>();
            Assert.IsNotNull(e);
        }
    }
}
