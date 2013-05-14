using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{ 
    public class TenantMaintenanceController : Controller
    {
        private readonly DB_33736_rentalEntities _db = new DB_33736_rentalEntities();

        public ViewResult Index()
        {
            var maintenanceorders = _db.MaintenanceOrders.Include(m => m.ServiceType).Include(m => m.UrgencyType);
             var userGUID = UserHelper.GetUserGUID();
            if (userGUID == null)
            {
                return View(maintenanceorders.ToList());
            }
            var tenantID = UserHelper.GetTenantID((Guid) userGUID);
            maintenanceorders = _db.MaintenanceOrders.Include(m => m.ServiceType).Include(m => m.UrgencyType).
                Where(t => t.TenantMaintenance.TenantID == tenantID);
            return View(maintenanceorders.ToList());
        }

        public ViewResult Details(int id)
        {
            var maintenanceorder = _db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        public ActionResult Create()
        {
            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1");
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        {
            if (ModelState.IsValid)
            {
                _db.MaintenanceOrders.Add(maintenanceorder);
                _db.SaveChanges();
                var userGUID = UserHelper.GetUserGUID();
                if (userGUID != null)
                {
                    var tenantID = UserHelper.GetTenantID((Guid) userGUID);
                    if (tenantID != null)
                    {
                        var tenantMaintenance = new TenantMaintenance
                                                    {
                                                        TenantID = (int) tenantID,
                                                        MaintenanceID = maintenanceorder.MaintenanceID,
                                                        MaintenanceOrder = maintenanceorder
                                                    };
                        _db.TenantMaintenances.Add(tenantMaintenance);
                    }
                }
                _db.SaveChanges();
                TempData["UserName"] = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                TempData["Id"] = maintenanceorder.MaintenanceID;
                TempData["Type"] = "Requests";
                return RedirectToAction("Index", "UploadMaintenancePhoto");
            }

            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        public ActionResult Edit(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        [HttpPost]
        public ActionResult Edit(MaintenanceOrder maintenanceorder)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(maintenanceorder).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        public ActionResult Delete(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            TenantMaintenance tenantmaintenance = _db.TenantMaintenances.FirstOrDefault(x => x.MaintenanceID == id);
            _db.MaintenanceOrders.Remove(maintenanceorder);
            _db.TenantMaintenances.Remove(tenantmaintenance);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddMorePhotos([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        {
            TempData["UserName"] = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            TempData["Id"] = maintenanceorder.MaintenanceID;
            TempData["Type"] = "Requests";
            return RedirectToAction("Index", "AddMaintenancePhoto");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}