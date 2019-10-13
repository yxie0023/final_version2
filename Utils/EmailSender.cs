using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace final_version2.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        /*public void SendEmails(String[] toEmail, String subject, String contents)
        {
            for (int i = 0 ; i < toEmail.Length; i++)
            {
                Send2(toEmail[i], subject, contents);
            }
        }
        */

            /*
        public static void SendEmailWithAttachment(String filePath, String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("test@localhost.com");

          
                var to = new EmailAddress(toEmail);
                var body = contents;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
                var bytes = File.ReadAllBytes(filePath);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment("file.txt", file);
                var response = client.SendEmailAsync(msg);

            
        }
        */
    }
}