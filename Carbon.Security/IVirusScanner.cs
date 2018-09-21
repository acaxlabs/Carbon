using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Security
{
    public interface IVirusScanner
    {
        bool IsVirus(Stream stream);
        bool IsVirus(byte[] byteArray);
    }
}
