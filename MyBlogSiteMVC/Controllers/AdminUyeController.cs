using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogSiteMVC.Models;

namespace MyBlogSiteMVC.Controllers
{
    public class AdminUyeController : Controller
    {
        private ademkal1_MyBlogSiteEntities db = new ademkal1_MyBlogSiteEntities();

        // GET: AdminUye
        public ActionResult Index()
        {
            return View(db.Uyes.ToList());
        }

        // GET: AdminUye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: AdminUye/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UyeID,UyeAdSoyad,UyeMail,UyeSifre")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Uyes.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uye);
        }

        // GET: AdminUye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: AdminUye/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UyeID,UyeAdSoyad,UyeMail,UyeSifre")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uye);
        }

        // GET: AdminUye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: AdminUye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = db.Uyes.Find(id);
            db.Uyes.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uye uye)
        {
            
            var login = db.Uyes.Where(u => u.UyeMail == uye.UyeMail && u.UyeSifre == uye.UyeSifre).FirstOrDefault();

            if (login!=null)
            {
                Session["UyeID"] = login.UyeID;
                return RedirectToAction("Index", "AdminBlog");
            }
            else
            {
                ViewBag.HataliGiris = "Hatalı Giriş Yaptınız, Kontrol Edip Tekrar Deneyin...";
                return View();

            }
            
            
        }
        public ActionResult Logaout()
        {

            Session["UyeID"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Blog");
        }
    }
}
