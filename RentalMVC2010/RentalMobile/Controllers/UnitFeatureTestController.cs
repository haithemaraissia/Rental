using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{ 
    public class UnitFeatureTestController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /UnitFeatureTest/

        public ViewResult Index()
        {
            var unitfeatures = db.UnitFeatures.Include(u => u.Unit);
            return View(unitfeatures.ToList());
        }

        //
        // GET: /UnitFeatureTest/Details/5

        public ViewResult Details(int id)
        {
            UnitFeature unitfeature = db.UnitFeatures.Find(id);
            return View(unitfeature);
        }

        //
        // GET: /UnitFeatureTest/Create

        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Description");
            return View();
        } 

        //
        // POST: /UnitFeatureTest/Create

        [HttpPost]
        public ActionResult Create(UnitFeature unitfeature)
        {
            if (ModelState.IsValid)
            {
                db.UnitFeatures.Add(unitfeature);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Description", unitfeature.UnitId);
            return View(unitfeature);
        }
        
        //
        // GET: /UnitFeatureTest/Edit/5
 
        public ActionResult Edit(int id)
        {
            UnitFeature unitfeature = db.UnitFeatures.Find(id);
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Description", unitfeature.UnitId);
            return View(unitfeature);
        }

        //
        // POST: /UnitFeatureTest/Edit/5

        [HttpPost]
        public ActionResult Edit(UnitFeature unitfeature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitfeature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Description", unitfeature.UnitId);
            return View(unitfeature);
        }

        //
        // GET: /UnitFeatureTest/Delete/5
 
        public ActionResult Delete(int id)
        {
            UnitFeature unitfeature = db.UnitFeatures.Find(id);
            return View(unitfeature);
        }

        //
        // POST: /UnitFeatureTest/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            UnitFeature unitfeature = db.UnitFeatures.Find(id);
            db.UnitFeatures.Remove(unitfeature);
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