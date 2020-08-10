using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Email
{

    public class SendGridClient : SmtpClient, ISmtpProvider
    {
        public SendGridClient()
        {
            var hasPort = int.TryParse(ConfigurationManager.AppSettings["Carbon.Smtp.Port"], out int port);
            Host = ConfigurationManager.AppSettings["Carbon.Smtp.Host"];// host;
            Port = hasPort ? port : 587;
            Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["Carbon.Smtp.Username"], 
                ConfigurationManager.AppSettings["Carbon.Smtp.Password"]);
        }

        public SendGridClient(string host, string username, string password, int port = 587)
        {
            Host = host;
            Port = port;
            Credentials = new NetworkCredential(username, password);
        }

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

        public void SendHtmlMessage(string replyto, string from, string to, string cc, string subject, string htmlBody, Attachment attachment)
        {
            MailMessage msg = new MailMessage(from, to, subject, htmlBody);
            if (!string.IsNullOrEmpty(replyto))
            {
                msg.ReplyToList.Add(new MailAddress(replyto));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                msg.CC.Add(new MailAddress(cc));
            }
            if (attachment != null)
                msg.Attachments.Add(attachment);

            msg.IsBodyHtml = true;
            SendMessage(msg);
        }

        public void SendMessage(MailMessage msg)
        {
            try
            {
                this.Send(msg);
            }
            catch (Exception)
            {
                // fail gracefully
            }
        }
    }

}
