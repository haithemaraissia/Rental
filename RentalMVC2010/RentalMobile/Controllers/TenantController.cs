using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{ 
    [Authorize]
    public class TenantController : Controller
    {

        public DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Tenant/
        //GET: CurrentTenant

        public ViewResult Index()
        {
            Tenant tenant = db.Tenants.Find(UserHelper.GetTenantID());
            ViewBag.TenantProfile = tenant;
            ViewBag.TenantId = tenant.TenantId;
            ViewBag.TenantGoogleMap = string.IsNullOrEmpty(tenant.Address) ? "USA" : tenant.Address;
            
            return View(tenant);
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



    // Delete All associated records

            var tenantshowing = db.TenantShowings.Where(x => x.TenantId == id).ToList();
            foreach (var x in tenantshowing)
            {
                db.TenantShowings.Remove(x);
            }
            db.SaveChanges();


        

            //Delete from Membership

            if (Roles.GetRolesForUser(User.Identity.Name).Any())
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser(User.Identity.Name));
            }
            Membership.DeleteUser(User.Identity.Name);
            FormsAuthentication.SignOut();

            return RedirectToAction("Index","Home");
        }













        public ActionResult UpdateProfilePicture(int id)
        {
            return RedirectToAction("Upload","Account",new {id = id});
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