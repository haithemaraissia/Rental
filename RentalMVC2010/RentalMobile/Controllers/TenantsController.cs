using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class TenantsController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var tenant = db.Tenants.Find(UserHelper.GetTenantID(id));
            ViewBag.tenantProfile = tenant;
            ViewBag.tenantId = tenant.TenantId;
            ViewBag.tenantGoogleMap = tenant.GoogleMap;
            return View(tenant);
        }

    }
}
