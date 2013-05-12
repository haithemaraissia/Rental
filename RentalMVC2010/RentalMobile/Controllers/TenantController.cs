using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{ 
    public class TenantController : Controller
    {
        public DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Tenant/
        //GET: CurrentTenant
        public ViewResult Index()
        {
            var tenantFavorite = (from t in db.TenantFavorites
                                  where t.TenantId == UserHelper.GetTenantID()
                                     select t) ;
            ViewBag.TenantProfile = tenantFavorite;
            return View(db.Tenants.ToList());
        }

        //
        // GET: /Tenant/Details/5

        public ViewResult Details(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            return View(tenant);


         //   TenantModelView ModelView = new TenantModelView{ Tenants}


            //UnitModelView myview = new UnitModelView { Units = db.Units.Find(id) };

            //UnitGallery unitgallery = db.UnitGalleries.Find(id);


            //IQueryable<UnitGallery> test = (from d in db.UnitGalleries
            //                                where d.UnitId == id
            //                                select d);


            //myview.UnitGalleries = test.ToList();
            ////var x = db.UnitGalleries.Where(x => x.UnitId == id);


            //return View(myview);
        }

        //
        // GET: /Tenant/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tenant/Create

        [HttpPost]
        public ActionResult Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Tenants.Add(tenant);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tenant);
        }
        
        //
        // GET: /Tenant/Edit/5
 
        public ActionResult Edit(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            return View(tenant);
        }

        //
        // POST: /Tenant/Edit/5

        [HttpPost]
        public ActionResult Edit(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        //
        // GET: /Tenant/Delete/5
 
        public ActionResult Delete(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            return View(tenant);
        }

        //
        // POST: /Tenant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
















        //DETAIL OF TENANT FAVORITE

        //public PartialViewResult FavoriteDetails(int id)
        //{

        //    var tenantfavorite =  db.TenantFavorites.Where(x => x.TenantId == 2 && x.FavoriteId == id).FirstOrDefault();
        //    //Tenant tenant = db.TenantFavorites.Where(Tenant == 6 && )
        //    return PartialView("_TenantFavDetail",tenantfavorite);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}