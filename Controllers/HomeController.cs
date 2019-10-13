using final_version2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using final_version2.Utils;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace final_version2.Controllers
{
    public class HomeController : Controller
    {
        [RequireHttps]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home
        public ActionResult Send_Email()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            var emailList = model.To.Split(';');
            foreach(string toEmail in emailList)
            { 
                using (MailMessage mm = new MailMessage(model.Email, toEmail))
                {
                    mm.Subject = model.Subject;
                    mm.Body = model.Body;

                    if (model.Attachment.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(model.Attachment.FileName);
                        mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                    }

                    mm.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        ViewBag.Message = "Email sent.";
                    }
                }
            }
            return View();
        }

    }
}