using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{ 
    public class MaintenanceOrderController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /MaintenanceOrder/

        public ViewResult Index()
        {
            var maintenanceorders = db.MaintenanceOrders.Include(m => m.ServiceType).Include(m => m.UrgencyType);
                //Include(k => k.TenantMaintenances.Where(t => t.TenantID == 1));

               return View(maintenanceorders.ToList());
        }

        //
        // GET: /MaintenanceOrder/Details/5

        public ViewResult Details(int id)
        {
            MaintenanceOrder maintenanceorder = db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        //
        // GET: /MaintenanceOrder/Create

        public ActionResult Create()
        {
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "ServiceType1");
            ViewBag.UrgencyID = new SelectList(db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1");



            ViewBag.TenantUserName = "Jack";
            return View();
        }


        public ActionResult Create2()
        {
            return View();
        } 


        //
        // POST: /MaintenanceOrder/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        {

            if (ModelState.IsValid)
            {
                db.MaintenanceOrders.Add(maintenanceorder);
                db.SaveChanges();


                TempData["TenantUsername"] = "mike";
                TempData["RequestID"] = maintenanceorder.MaintenanceID;

                //Maybe we don't need to pass the entire model
                //TempData["MaintenanceOrderModel"] = maintenanceorder;
                //Maybe we don't need to pass the entire model
                return RedirectToAction("Index","Upload"); 
            }

            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }
        
        //
        // GET: /MaintenanceOrder/Edit/5
 
        public ActionResult Edit(int id)
        {
            MaintenanceOrder maintenanceorder = db.MaintenanceOrders.Find(id);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        //
        // POST: /MaintenanceOrder/Edit/5

        [HttpPost]
        public ActionResult Edit(MaintenanceOrder maintenanceorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        //
        // GET: /MaintenanceOrder/Delete/5
 
        public ActionResult Delete(int id)
        {
            MaintenanceOrder maintenanceorder = db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        //
        // POST: /MaintenanceOrder/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            MaintenanceOrder maintenanceorder = db.MaintenanceOrders.Find(id);
            db.MaintenanceOrders.Remove(maintenanceorder);
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