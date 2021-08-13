using nClam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Security.Clam
{
    public class Scanner : IVirusScanner
    {
        public string HostNameOrAddress
        {
            get
            {
                return this._hostNameOrAddress;
            }
            set
            {
                this._hostNameOrAddress = value;
                _scanner = new ClamClient(Dns.GetHostEntry(_hostNameOrAddress).AddressList.FirstOrDefault().ToString());

            }
        }
        private string _hostNameOrAddress;
        private ClamClient _scanner;

        public Scanner()
        {
        }

        public Scanner(string serverIP)
        {
            if (string.IsNullOrEmpty(serverIP))
            {
                throw new Exception("ServerIP not found");
            }
            _scanner = new ClamClient(serverIP);
        }

        public bool IsVirus(Stream stream)
        {
            var bytes = ToByteArray(stream);
            return IsVirus(bytes);
        }

        public bool IsVirus(byte[] byteArray)
        {
            switch (_scanner.SendAndScanFileAsync(byteArray).Result.Result)
            {
                case ClamScanResults.Clean: return false;
                case ClamScanResults.VirusDetected: return true;
                case ClamScanResults.Error: throw new Exception("Error during virus scan");
                default: return false;
            }
        }

        private static byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
