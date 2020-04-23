﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABS_v1.Models;

namespace ABS_v1.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.News.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsDate,NewsTitle,NewsDescription,Location,Image")] NewsModel newsModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var originalFilename = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);
                var newFileName = newsModel.NewsId.ToString() + ext;
                var path = Path.Combine(Server.MapPath(@"~/Uploads/News/"), newsModel.NewsId.ToString() + ext);
                file.SaveAs(path);
                newsModel.Image = newFileName;

                db.News.Add(newsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsModel);
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.News.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsDate,NewsTitle,NewsDescription,Location,Image")] NewsModel newsModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    var originalFilename = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    var newFileName = newsModel.NewsId.ToString() + ext;
                    var path = Path.Combine(Server.MapPath(@"~/Uploads/News/"), newsModel.NewsId.ToString() + ext);
                    file.SaveAs(path);
                    newsModel.Image = newFileName;

                }
                db.Entry(newsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsModel);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.News.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsModel newsModel = db.News.Find(id);
            db.News.Remove(newsModel);
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
