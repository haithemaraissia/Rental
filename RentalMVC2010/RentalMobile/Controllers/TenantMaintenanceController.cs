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
    public class TenantMaintenanceController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /TenantMaintenance/

        public ViewResult Index()
        {
            var tenantmaintenances = db.TenantMaintenances.Include(t => t.MaintenanceOrder).Where( t=> t.TenantID ==1 );

            return View(tenantmaintenances.ToList());
        }

        //
        // GET: /TenantMaintenance/Details/5

        public ViewResult Details(int id)
        {
            TenantMaintenance tenantmaintenance = db.TenantMaintenances.Find(id);
            return View(tenantmaintenance);
        }

        //
        // GET: /TenantMaintenance/Create

        public ActionResult Create()
        {
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description");
            return View();
        } 

        //
        // POST: /TenantMaintenance/Create

        [HttpPost]
        public ActionResult Create(TenantMaintenance tenantmaintenance)
        {
            if (ModelState.IsValid)
            {
                db.TenantMaintenances.Add(tenantmaintenance);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", tenantmaintenance.MaintenanceID);
            return View(tenantmaintenance);
        }
        
        //
        // GET: /TenantMaintenance/Edit/5
 
        public ActionResult Edit(int id)
        {
            TenantMaintenance tenantmaintenance = db.TenantMaintenances.Find(id);
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", tenantmaintenance.MaintenanceID);
            return View(tenantmaintenance);
        }

        //
        // POST: /TenantMaintenance/Edit/5

        [HttpPost]
        public ActionResult Edit(TenantMaintenance tenantmaintenance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenantmaintenance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description", tenantmaintenance.MaintenanceID);
            return View(tenantmaintenance);
        }

        //
        // GET: /TenantMaintenance/Delete/5
 
        public ActionResult Delete(int id)
        {
            TenantMaintenance tenantmaintenance = db.TenantMaintenances.Find(id);
            return View(tenantmaintenance);
        }

        //
        // POST: /TenantMaintenance/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TenantMaintenance tenantmaintenance = db.TenantMaintenances.Find(id);
            db.TenantMaintenances.Remove(tenantmaintenance);
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