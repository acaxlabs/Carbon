using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarbonTests.Email
{
    public class EmailBlocker : DomainBlocker
    {
        public bool IsValidEmail(List<string> excludedDomains, string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                if (excludedDomains.Any(dom => email.Split('@').Contains(dom)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
           

        }
    }
}
