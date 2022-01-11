using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using Carbon.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarbonTests.Email
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void IsValidEmail()
        {
            DomainBlocker p = Container.Get<DomainBlocker>();
            string email = "test@yahoo.com"; 
            List<string> excludedDomains = new List<string>()
            {
                "sharklasers.com",
                "gurilla.com"
            };
            Assert.IsTrue(p.IsValidEmail(excludedDomains, email));
        }
    }
}
