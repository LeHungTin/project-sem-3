using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ProjectQT.Web.Models
{
    public static class Helper
    {
        public static void SendMail(string toEmail, string fromEmail, string passEmail, string titleEmail, string contentEmail)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(fromEmail);
            mail.Subject = titleEmail;
            mail.Body = contentEmail;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, passEmail);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}