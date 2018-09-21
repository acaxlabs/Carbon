using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbon.Security.Clam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Carbon.Security.Clam.Tests
{
    [TestClass()]
    public class ScannerTests
    {
        IVirusScanner scanner = null;

        [TestMethod()]
        public void ScannerTest()
        {
            scanner = new Clam.Scanner("giclam.cloudapp.net");
        }

        [TestMethod()]
        public void IsVirusTest()
        {
            scanner = new Clam.Scanner("giclam.cloudapp.net");
            var file = File.Open("../../data/fake.js", FileMode.Open);
            var virus = scanner.IsVirus(file);

            Assert.IsFalse(virus);
        }
        
    }
}