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
    public class UnitController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Unit/

        public ViewResult Index()
        {
            return View(db.Units.ToList());
        }

        //
        // GET: /Unit/Details/5

        public ViewResult Details(int id)
        {
            Unit unit = db.Units.Find(id);
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Unit/Create

        [HttpPost]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(unit);
        }
        
        //
        // GET: /Unit/Edit/5
 
        public ActionResult Edit(int id)
        {
            Unit unit = db.Units.Find(id);
            return View(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        //
        // GET: /Unit/Delete/5
 
        public ActionResult Delete(int id)
        {
            Unit unit = db.Units.Find(id);
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
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