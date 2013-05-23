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
    public class OwnerController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        // GET: /Owner/

        public ViewResult Index()
        {
            var owner = db.Owners.Find(UserHelper.GetOwnerID());
            ViewBag.OwnerProfile = owner;
            ViewBag.OwnerId = owner.OwnerId;
            ViewBag.OwnerGoogleMap = owner.GoogleMap;
            return View(owner);
        }

        // GET: /Owner/Edit/5
 
        public ActionResult Edit(int id)
        {
            Owner owner = db.Owners.Find(id);
            return View(owner);
        }

        //
        // POST: /Owner/Edit/5

        [HttpPost]
        public ActionResult Edit(Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(owner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(owner);
        }


        // GET: /Owner/ChangeAddress/5

        public ActionResult ChangeAddress(int id)
        {
            Owner owner = db.Owners.Find(id);
            return View(owner);
        }

        //
        // POST: /Owner/ChangeAddress/5

        [HttpPost]
        public ActionResult ChangeAddress(Owner Owner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Owner).State = EntityState.Modified;
                Owner.GoogleMap = string.IsNullOrEmpty(Owner.Address) ? UserHelper.GetFormattedLocation("", "", "USA") : UserHelper.GetFormattedLocation(Owner.Address, Owner.City, Owner.CountryCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Owner);
        }

        // GET: /Owner/Delete/5
 
        public ActionResult Delete(int id)
        {
            Owner owner = db.Owners.Find(id);
            return View(owner);
        }

        //
        // POST: /Owner/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Owner owner = db.Owners.Find(id);
            db.Owners.Remove(owner);
            db.SaveChanges();


            //// Delete All associated records

            //var Ownershowing = db.OwnerShowings.Where(x => x.OwnerId == id).ToList();
            //foreach (var x in Ownershowing)
            //{
            //    db.OwnerShowings.Remove(x);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}