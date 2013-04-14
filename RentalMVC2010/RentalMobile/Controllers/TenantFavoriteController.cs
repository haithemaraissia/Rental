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
    public class TenantFavoriteController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /TenantFavorite/

        public ViewResult Index()
        {
            var tenantfavorites = db.TenantFavorites.Include(t => t.Tenant);
            return View(tenantfavorites.ToList());
        }

        //
        // GET: /TenantFavorite/Details/5

        public ViewResult Details(int id)
        {
            TenantFavorite tenantfavorite = db.TenantFavorites.Find(id);
            return View(tenantfavorite);
        }

        //
        // GET: /TenantFavorite/Create

        public ActionResult Create()
        {
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName");
            return View();
        } 

        //
        // POST: /TenantFavorite/Create

        [HttpPost]
        public ActionResult Create(TenantFavorite tenantfavorite)
        {
            if (ModelState.IsValid)
            {
                db.TenantFavorites.Add(tenantfavorite);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", tenantfavorite.TenantId);
            return View(tenantfavorite);
        }
        
        //
        // GET: /TenantFavorite/Edit/5
 
        public ActionResult Edit(int id)
        {
            TenantFavorite tenantfavorite = db.TenantFavorites.Find(id);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", tenantfavorite.TenantId);
            return View(tenantfavorite);
        }

        //
        // POST: /TenantFavorite/Edit/5

        [HttpPost]
        public ActionResult Edit(TenantFavorite tenantfavorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenantfavorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", tenantfavorite.TenantId);
            return View(tenantfavorite);
        }

        //
        // GET: /TenantFavorite/Delete/5
 
        public ActionResult Delete(int id)
        {
            TenantFavorite tenantfavorite = db.TenantFavorites.Find(id);
            return View(tenantfavorite);
        }

        //
        // POST: /TenantFavorite/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TenantFavorite tenantfavorite = db.TenantFavorites.Find(id);
            db.TenantFavorites.Remove(tenantfavorite);
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