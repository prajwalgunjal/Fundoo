using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class Send
    {
        public string SendingMail(string emailTo , string token)
        {
            string emailForm = "prajwalgunjal86@gmail.com";
            MailMessage message = new MailMessage(emailForm, emailTo);
            string mailBody = "Token Generated: " + token;
            message.Subject = "Generated Token will expire after 15 min";
            message.Body = mailBody.ToString();
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
            NetworkCredential credential = new NetworkCredential("prajwalgunjal86@gmail.com", "cdxmzfkzoyocltbp");

            smtpClient.EnableSsl = true;    
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;

            smtpClient.Send(message);
            //smtpClient.Send(emailForm, emailTo, subject, body);
            return emailTo;

        }

    }
}
