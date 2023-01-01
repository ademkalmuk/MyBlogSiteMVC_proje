using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogSiteMVC.Models
{
    public class MailModel
    {
        public string Email { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string DosyaYolu { get; set; }
    }
}