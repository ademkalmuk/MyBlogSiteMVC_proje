using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyBlogSiteMVC.Models;
using PagedList;
using System.IO;
using System.Net;

namespace MyBlogSiteMVC.Controllers
{
    public class BlogController : Controller
    {
        ademkal1_MyBlogSiteEntities db = new ademkal1_MyBlogSiteEntities();
        // GET: Blog
        public ActionResult Index(int page=1)
        {
            var blog = db.Blogs.OrderByDescending(b => b.BlogID).ToPagedList(page,5);

            return View(blog);
        }
        public ActionResult Anasayfa(int page = 1)
        {
            var blog = db.Blogs.OrderByDescending(b => b.BlogID).ToPagedList(page, 5);

            return View(blog);
        }
        public ActionResult Galeri(int page = 1)
        {
            var galeri = db.Görseller.OrderByDescending(g => g.GörselID).ToPagedList(page, 20);

            return View(galeri);
        }

        public ActionResult Hakkinda()
        {
            var hakkinda = db.Hakkindas.ToList();
            return View(hakkinda);
        }

        public ActionResult BlogDetay(int id)
        {
            var blogDetay = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
            if (blogDetay==null)
            {
                return HttpNotFound();
            }

            return View(blogDetay);
        }
        public ActionResult OkunmaArttir(int blogid)
        {
            var blog = db.Blogs.Where(b => b.BlogID == blogid).SingleOrDefault();
            blog.BlogOkunmaSayisi = blog.BlogOkunmaSayisi + 1;
            db.SaveChanges();
            return View();
        }         
        
        [HttpGet]
        public ActionResult BizeUlasın()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BizeUlasın(MailModel mailModel, HttpPostedFileBase fileBase )
        {
            if (ModelState.IsValid)
            {
                string from = "ademkalmuk@hotmail.com";
                using (MailMessage mail = new MailMessage(from, from))
                {
                    mail.SubjectEncoding = Encoding.Default;
                    mail.Subject = mailModel.Konu;
                    mail.BodyEncoding = Encoding.Default;
                    mail.Body = "Ad Soyad: " + mailModel.AdSoyad + ".\n" + "Mail Adresi: " + mailModel.Email +
                        ".\n" + "Telefon Numarası: " + mailModel.Telefon + ".\n"+"Mesaj: "+mailModel.Mesaj;

                    if (fileBase != null)
                    {
                        mailModel.DosyaYolu = Path.GetFileName(fileBase.FileName);// dosyanın yolunu aldık
                        mail.Attachments.Add(new Attachment(fileBase.InputStream, mailModel.DosyaYolu)); //dosta yükleme işlemi

                    }
                    using (var smtp=new SmtpClient())
                    {
                        var credentinal = new NetworkCredential
                        {
                            UserName = "ademkalmuk@hotmail.com",
                            Password = "147258*a"

                        };
                        smtp.Credentials = credentinal;
                        smtp.Host = "smtp-mail.outlook.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        ViewBag.Gonder = "Mesajınız Başarıyla Gönderildi.";
                        return View();
                    }
                }


            }
            return View();
        }


    }

}