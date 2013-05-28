using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class OwnersController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var owner = db.Owners.Find(UserHelper.GetOwnerID(id));
            ViewBag.ownerProfile = owner;
            ViewBag.ownerId = owner.OwnerId;
            ViewBag.ownerGoogleMap = owner.GoogleMap;
            return View(owner);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
