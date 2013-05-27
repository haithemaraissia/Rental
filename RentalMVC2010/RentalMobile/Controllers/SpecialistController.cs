using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    [Authorize]
    public class SpecialistController : Controller
    {

        public DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Specialist/
        //GET: CurrentSpecialist

        public ViewResult Index()
        {
            var specialist = db.Specialists.Find(UserHelper.GetSpecialistID());
            ViewBag.SpecialistProfile = specialist;
            ViewBag.SpecialistId = specialist.SpecialistId;
            ViewBag.SpecialistGoogleMap = specialist.GoogleMap;
            return View(specialist);
        }


        // GET: /Specialist/Edit/5

        public ActionResult Edit(int id)
        {
            Specialist Specialist = db.Specialists.Find(id);
            return View(Specialist);
        }

        //
        // POST: /Specialist/Edit/5

        [HttpPost]
        public ActionResult Edit(Specialist Specialist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Specialist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Specialist);
        }


        //
        // GET: /Specialist/Edit/5

        public ActionResult ChangeAddress(int id)
        {
            Specialist Specialist = db.Specialists.Find(id);
            return View(Specialist);
        }

        //
        // POST: /Specialist/Edit/5

        [HttpPost]
        public ActionResult ChangeAddress(Specialist Specialist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Specialist).State = EntityState.Modified;
                Specialist.GoogleMap = string.IsNullOrEmpty(Specialist.Address) ? UserHelper.GetFormattedLocation("", "", "USA") : UserHelper.GetFormattedLocation(Specialist.Address, Specialist.City, Specialist.CountryCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Specialist);
        }

        //
        // GET: /Specialist/Delete/5

        public ActionResult Delete(int id)
        {
            Specialist Specialist = db.Specialists.Find(id);
            return View(Specialist);
        }

        //
        // POST: /Specialist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialist Specialist = db.Specialists.Find(id);
            db.Specialists.Remove(Specialist);
            db.SaveChanges();



            //// Delete All associated records

            //var Specialistshowing = db.SpecialistShowings.Where(x => x.SpecialistId == id).ToList();
            //foreach (var x in Specialistshowing)
            //{
            //    db.SpecialistShowings.Remove(x);
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


        //DETAIL OF Specialist FAVORITE

        //public PartialViewResult FavoriteDetails(int id)
        //{

        //    var Specialistfavorite =  db.SpecialistFavorites.Where(x => x.SpecialistId == 2 && x.FavoriteId == id).FirstOrDefault();
        //    //Specialist Specialist = db.SpecialistFavorites.Where(Specialist == 6 && )
        //    return PartialView("_SpecialistFavDetail",Specialistfavorite);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
