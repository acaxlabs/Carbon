using Carbon.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Smtp
{
    public static class EmailBlocker
    {
        public static bool IsValidEmail(string email)
        {
           var excludedDomains= ConfigurationManager.AppSettings["Carbon.Email.ExcludedDomains"].Split(',');
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
