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
    public class AdminHakkindaController : Controller
    {
        private ademkal1_MyBlogSiteEntities db = new ademkal1_MyBlogSiteEntities();

        // GET: AdminHakkinda
        public ActionResult Index()
        {
            return View(db.Hakkindas.ToList());
        }

        // GET: AdminHakkinda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkinda hakkinda = db.Hakkindas.Find(id);
            if (hakkinda == null)
            {
                return HttpNotFound();
            }
            return View(hakkinda);
        }

        // GET: AdminHakkinda/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HakkindaID,HakkindaBaslik,HakkindaIcerik")] Hakkinda hakkinda)
        {
            if (ModelState.IsValid)
            {
                db.Hakkindas.Add(hakkinda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hakkinda);
        }

        // GET: AdminHakkinda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkinda hakkinda = db.Hakkindas.Find(id);
            if (hakkinda == null)
            {
                return HttpNotFound();
            }
            return View(hakkinda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HakkindaID,HakkindaBaslik,HakkindaIcerik")] Hakkinda hakkinda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hakkinda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hakkinda);
        }

        // GET: AdminHakkinda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkinda hakkinda = db.Hakkindas.Find(id);
            if (hakkinda == null)
            {
                return HttpNotFound();
            }
            return View(hakkinda);
        }

        // POST: AdminHakkinda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hakkinda hakkinda = db.Hakkindas.Find(id);
            db.Hakkindas.Remove(hakkinda);
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
    }
}
