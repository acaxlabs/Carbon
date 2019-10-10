using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Smtp
{
    public interface ISmtpProvider
    {
        void SendHtmlMessage(string replyto,
          string from,
          string to,
          string cc, string subject, string htmlBody, Attachment attachment);

        void SendMessage(MailMessage msg);
    }
}
