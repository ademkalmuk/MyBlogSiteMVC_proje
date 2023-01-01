using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlogSiteMVC.Models;
using System.Web.Helpers;
using System.IO;
using PagedList;

namespace MyBlogSiteMVC.Controllers
{
    public class GörsellerController : Controller
    {
        ademkal1_MyBlogSiteEntities db = new ademkal1_MyBlogSiteEntities();
        // GET: Görseller

        public ActionResult Index(int page=1)
        {
            var görsel = db.Görseller.OrderByDescending(g => g.GörselID).ToPagedList(page, 10);
            return View(görsel);
        }

        // GET: Görseller/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Görseller görsel = db.Görseller.Find(id);
            if (görsel == null)
            {
                return HttpNotFound();
            }
            return View(görsel);
        }

        // GET: Görseller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Görseller/Create
        [HttpPost]
        public ActionResult Create(Görseller görseller)
        {
            try
            {
                if (Request.Files.Count > 0)
                {

                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string uzantıyolu = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Content/Uploads/" + dosyaadi + uzantıyolu;

                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    görseller.Görsel = "/Content/Uploads/" + dosyaadi+uzantıyolu;
                }

                görseller.GörselYayınlanmaTarih = DateTime.Now;
                db.Görseller.Add(görseller);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Görseller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Görseller/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Görseller/Delete/5
        public ActionResult Delete(int id)
        {
            var görseller = db.Görseller.Where(g => g.GörselID == id).SingleOrDefault();
            if (görseller == null)
            {
                return HttpNotFound();
            }
            return View(görseller);
        }

        // POST: Görseller/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var görseller = db.Görseller.Where(g => g.GörselID == id).SingleOrDefault();
                if (görseller == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(görseller.Görsel)))
                {
                    System.IO.File.Delete(Server.MapPath(görseller.Görsel));
                }
                db.Görseller.Remove(görseller);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
