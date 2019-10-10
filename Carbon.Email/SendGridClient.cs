using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Smtp
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
