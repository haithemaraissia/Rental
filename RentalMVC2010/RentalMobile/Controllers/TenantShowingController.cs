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
    public class TenantShowingController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /TenantShowing/

        public ViewResult Index()
        {
            return View(db.TenantShowings.ToList());
        }

        //
        // GET: /TenantShowing/Details/5

        public ViewResult Details(int id)
        {
            TenantShowing tenantshowing = db.TenantShowings.Find(id);
            return View(tenantshowing);
        }

        //
        // GET: /TenantShowing/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TenantShowing/Create

        [HttpPost]
        public ActionResult Create(TenantShowing tenantshowing)
        {
            if (ModelState.IsValid)
            {
                db.TenantShowings.Add(tenantshowing);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tenantshowing);
        }
        
        //
        // GET: /TenantShowing/Edit/5
 
        public ActionResult Edit(int id)
        {
            TenantShowing tenantshowing = db.TenantShowings.Find(id);
            return View(tenantshowing);
        }

        //
        // POST: /TenantShowing/Edit/5

        [HttpPost]
        public ActionResult Edit(TenantShowing tenantshowing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenantshowing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenantshowing);
        }

        //
        // GET: /TenantShowing/Delete/5
 
        public ActionResult Delete(int id)
        {
            TenantShowing tenantshowing = db.TenantShowings.Find(id);
            return View(tenantshowing);
        }

        //
        // POST: /TenantShowing/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TenantShowing tenantshowing = db.TenantShowings.Find(id);
            db.TenantShowings.Remove(tenantshowing);
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