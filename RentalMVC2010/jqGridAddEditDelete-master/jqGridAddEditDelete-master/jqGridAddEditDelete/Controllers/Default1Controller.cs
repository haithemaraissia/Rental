using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jqGridAddEditDelete.Models;

namespace jqGridAddEditDelete.Controllers
{ 
    public class Default1Controller : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            var unitgalleries = db.UnitGalleries.Include("Unit");
            return View(unitgalleries.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Single(u => u.UnitGalleryId == id);
            return View(unitgallery);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address");
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(UnitGallery unitgallery)
        {
            if (ModelState.IsValid)
            {
                db.UnitGalleries.AddObject(unitgallery);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Single(u => u.UnitGalleryId == id);
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(UnitGallery unitgallery)
        {
            if (ModelState.IsValid)
            {
                db.UnitGalleries.Attach(unitgallery);
                db.ObjectStateManager.ChangeObjectState(unitgallery, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Address", unitgallery.UnitId);
            return View(unitgallery);
        }

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            UnitGallery unitgallery = db.UnitGalleries.Single(u => u.UnitGalleryId == id);
            return View(unitgallery);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            UnitGallery unitgallery = db.UnitGalleries.Single(u => u.UnitGalleryId == id);
            db.UnitGalleries.DeleteObject(unitgallery);
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