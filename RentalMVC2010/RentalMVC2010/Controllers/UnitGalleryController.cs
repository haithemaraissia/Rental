using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMVC2010.Models;
using PagedList;

namespace RentalMVC2010.Controllers
{
    public class UnitGalleryController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /UnitGallery/

        public ViewResult Index(int? page, string sortBy)
        {
            ViewBag.UnitSort = String.IsNullOrEmpty(sortBy) ? "UnitGalleryId desc" : "";
            ViewBag.GallerySort = sortBy == "UnitId" ? "UnitId desc" : "UnitId";

            var unitgalleries = db.UnitGalleries.Include(u => u.Unit).OrderBy(u => u.UnitGalleryId);

            switch (sortBy)
            {
                case "UnitGalleryId desc":
                    unitgalleries = unitgalleries.OrderByDescending(s => s.UnitGalleryId);
                    break;

                case "UnitGalleryId":
                    unitgalleries = unitgalleries.OrderBy(s => s.UnitGalleryId);
                    break;

                case "UnitId desc":
                    unitgalleries = unitgalleries.OrderByDescending(s => s.UnitId);
                    break;

                case "UnitId c":
                    unitgalleries = unitgalleries.OrderBy(s => s.UnitId);
                    break;


                default:
                    unitgalleries = unitgalleries.OrderByDescending(s => s.UnitGalleryId);
                    break;
            }



            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(unitgalleries.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /UnitGallery/Details/5

        public ViewResult Details(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Find(id);
            return View(unitgallery);
        }

        //
        // GET: /UnitGallery/Create

        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address");
            return View();
        }

        //
        // POST: /UnitGallery/Create

        [HttpPost]
        public ActionResult Create(UnitGallery unitgallery)
        {
            if (ModelState.IsValid)
            {
                db.UnitGalleries.Add(unitgallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }

        //
        // GET: /UnitGallery/Edit/5

        public ActionResult Edit(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Find(id);
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }

        //
        // POST: /UnitGallery/Edit/5

        [HttpPost]
        public ActionResult Edit(UnitGallery unitgallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitgallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }

        //
        // GET: /UnitGallery/Delete/5

        public ActionResult Delete(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Find(id);
            return View(unitgallery);
        }

        //
        // POST: /UnitGallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Find(id);
            db.UnitGalleries.Remove(unitgallery);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}