﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalMobile.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/
        
        public ActionResult Index()
        {
            return View();
        }











        public ActionResult TenantProfile()
        {
            return View();
        }

        public ActionResult OwnerProfile()
        {
            return View();
        }

        public ActionResult MaintenanceProfile()
        {
            return View();
        }

    }
}
