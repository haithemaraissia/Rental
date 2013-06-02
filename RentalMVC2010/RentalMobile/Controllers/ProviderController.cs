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
    public class ProviderController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Provider/
        //GET: CurrentProvider

        public ViewResult Index()
        {
            var Provider = db.MaintenanceProviders.Find(UserHelper.GetProviderID());
            ViewBag.ProviderProfile = Provider;
            ViewBag.ProviderId = Provider.MaintenanceProviderId;
            ViewBag.ProviderGoogleMap = Provider.GoogleMap;
            return View(Provider);
        }


        // GET: /Provider/Edit/5

        public ActionResult Edit(int id)
        {
            MaintenanceProvider Provider = db.MaintenanceProviders.Find(id);
            return View(Provider);
        }

        //
        // POST: /Provider/Edit/5

        [HttpPost]
        public ActionResult Edit(MaintenanceProvider Provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Provider);
        }


        //
        // GET: /Provider/Edit/5

        public ActionResult ChangeAddress(int id)
        {
            MaintenanceProvider Provider = db.MaintenanceProviders.Find(id);
            return View(Provider);
        }

        //
        // POST: /Provider/Edit/5

        [HttpPost]
        public ActionResult ChangeAddress(MaintenanceProvider Provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Provider).State = EntityState.Modified;
                Provider.GoogleMap = string.IsNullOrEmpty(Provider.Address) ? UserHelper.GetFormattedLocation("", "", "USA") : UserHelper.GetFormattedLocation(Provider.Address, Provider.City, Provider.CountryCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Provider);
        }

        //
        // GET: /Provider/Delete/5

        public ActionResult Delete(int id)
        {
            MaintenanceProvider Provider = db.MaintenanceProviders.Find(id);
            return View(Provider);
        }

        //
        // POST: /Provider/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceProvider Provider = db.MaintenanceProviders.Find(id);
            db.MaintenanceProviders.Remove(Provider);
            db.SaveChanges();



            //// Delete All associated records

            //var Providershowing = db.ProviderShowings.Where(x => x.ProviderId == id).ToList();
            //foreach (var x in Providershowing)
            //{
            //    db.ProviderShowings.Remove(x);
            //}
            //db.SaveChanges();




            //Delete from Membership

            if (Roles.GetRolesForUser(User.Identity.Name).Any())
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser(User.Identity.Name));
            }
            Membership.DeleteUser(User.Identity.Name);
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult UpdateProfilePicture(int id)
        {
            return RedirectToAction("Upload", "Account", new { id });
        }


        //DETAIL OF Provider FAVORITE

        //public PartialViewResult FavoriteDetails(int id)
        //{

        //    var Providerfavorite =  db.ProviderFavorites.Where(x => x.ProviderId == 2 && x.FavoriteId == id).FirstOrDefault();
        //    //Provider Provider = db.ProviderFavorites.Where(Provider == 6 && )
        //    return PartialView("_ProviderFavDetail",Providerfavorite);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}