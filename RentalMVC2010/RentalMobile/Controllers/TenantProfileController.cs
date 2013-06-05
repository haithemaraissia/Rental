using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class TenantProfileController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var Tenant = db.Tenants.Find(UserHelper.GetTenantID(id));
            ViewBag.TenantProfile = Tenant;
            ViewBag.TenantId = Tenant.TenantId;
            ViewBag.TenantGoogleMap = Tenant.GoogleMap;
            return View(Tenant);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
