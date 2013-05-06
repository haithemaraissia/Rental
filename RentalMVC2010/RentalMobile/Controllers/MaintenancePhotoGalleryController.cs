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
    public class MaintenancePhotoGalleryController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /MaintenancePhotoGallery/

        public ViewResult Index()
        {
            var maintenancephotoes = db.MaintenancePhotoes.Include(m => m.MaintenanceOrder);
            return View(maintenancephotoes.ToList());
        }

        //
        // GET: /MaintenancePhotoGallery/Details/5

        public ViewResult Details(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            return View(maintenancephoto);
        }

        //
        // GET: /MaintenancePhotoGallery/Create

        public ActionResult Create()
        {
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description");
            return View();
        } 

        //
        // POST: /MaintenancePhotoGallery/Create

        [HttpPost]
        public ActionResult Create(MaintenancePhoto maintenancephoto)
        {
            if (ModelState.IsValid)
            {
                db.MaintenancePhotoes.Add(maintenancephoto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }
        
        //
        // GET: /MaintenancePhotoGallery/Edit/5
 
        public ActionResult Edit(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }

        //
        // POST: /MaintenancePhotoGallery/Edit/5

        [HttpPost]
        public ActionResult Edit(MaintenancePhoto maintenancephoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenancephoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }

        //
        // GET: /MaintenancePhotoGallery/Delete/5
 
        public ActionResult Delete(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            return View(maintenancephoto);
        }

        //
        // POST: /MaintenancePhotoGallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            db.MaintenancePhotoes.Remove(maintenancephoto);
            db.SaveChanges();
            return RedirectToAction("Index","MaintenanceOrder");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}