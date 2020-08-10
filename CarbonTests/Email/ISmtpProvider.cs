using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonTests.Email
{
    interface ISmtpProvider
    {
        bool IsValidEmail(List<string> excludedDomains, string email);
    }
}
