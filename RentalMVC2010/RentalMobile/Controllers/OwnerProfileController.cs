using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class OwnerProfileController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var Owner = db.Owners.Find(UserHelper.GetOwnerID(id));
            ViewBag.OwnerProfile = Owner;
            ViewBag.OwnerId = Owner.OwnerId;
            ViewBag.OwnerGoogleMap = Owner.GoogleMap;
            return View(Owner);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
