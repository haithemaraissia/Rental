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
    public class ProfessionalsController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var specialist = db.Specialists.Find(UserHelper.GetSpecialistID(id));
            ViewBag.SpecialistProfile = specialist;
            ViewBag.SpecialistId = specialist.SpecialistId;
            ViewBag.SpecialistGoogleMap = specialist.GoogleMap;
            return View(specialist);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}