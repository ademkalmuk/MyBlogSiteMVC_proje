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
    public class AdminBlogController : Controller
    {
        ademkal1_MyBlogSiteEntities db = new ademkal1_MyBlogSiteEntities();
        // GET: AdminBlog
        public ActionResult Index(int page= 1)
        {

            var blog = db.Blogs.OrderByDescending(b => b.BlogID).ToPagedList(page, 10);
            return View(blog);
        }

        // GET: AdminBlog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: AdminBlog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBlog/Create
        [HttpPost]
        public ActionResult Create(Blog blog,HttpPostedFileBase Foto)
        {
            try
            {
                if (Foto!=null)
                {
                    WebImage webImage = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Content/Uploads/" + newfoto);
                    blog.Foto = "/Content/Uploads/" + newfoto;
                }
                blog.BlogOkunmaSayisi = 0;
                blog.BlogTarih = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminBlog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminBlog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Blog blog)
        {
            try
            {
                var blogGüncelle = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
                if (Foto!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(blogGüncelle.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(blogGüncelle.Foto));
                    }
                    WebImage webImage = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Content/Uploads/" + newfoto);
                    blogGüncelle.Foto = "/Content/Uploads/" + newfoto;
                    blogGüncelle.BlogBaslik = blog.BlogBaslik;
                    blogGüncelle.BlogIcerik = blog.BlogIcerik;
                    blogGüncelle.BlogOkunmaSayisi = blog.BlogOkunmaSayisi;
                    blogGüncelle.BlogOkunmaSuresi = blog.BlogOkunmaSuresi;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminBlog/Delete/5
        public ActionResult Delete(int id)
        {
            var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminBlog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
                if (blog == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(blog.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(blog.Foto));
                }
                db.Blogs.Remove(blog);
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
