using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace UtilityLayer
{
    public static class EmailHelper
    {
        public static void SendEmail(IConfiguration config, string toEmail, string subject, string body)
        {
            try
            {
                var appSettings = config.GetSection("EmailSettings");

                string fromEmail = appSettings["FromEmail"];
                string fromPassword = appSettings["Password"];
                string host = appSettings["Host"];
                int port = int.Parse(appSettings["Port"]);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while sending email: " + ex.Message);
            }
        }
    }
}
