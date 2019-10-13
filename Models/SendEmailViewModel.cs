using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace final_version2.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please enter an email address.")]
       
        public string To { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the body")]
        public string Body { get; set; }

        public HttpPostedFileBase Attachment { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}