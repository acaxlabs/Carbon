using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonTests.Email
{
    interface DomainBlocker
    {
        bool IsValidEmail(List<string> excludedDomains, string email);
    }
}
