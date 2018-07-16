using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Extensions
{
    public static class GuidExtensions
    {
        public static string Shorten(this Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes);
        }
    }
}
