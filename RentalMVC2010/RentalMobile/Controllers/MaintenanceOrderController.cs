using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class MaintenanceOrderController : Controller
    {
        private readonly DB_33736_rentalEntities _db = new DB_33736_rentalEntities();

        // GET: /MaintenanceOrder/
        public ViewResult Index()
        {
            var maintenanceorders = _db.MaintenanceOrders.Include(m => m.ServiceType).Include(m => m.UrgencyType);
            // maintenanceorders = maintenanceorders.Where(m => m.TenantMaintenances.First().TenantID == 2);
            //Include(k => k.TenantMaintenances.Where(t => t.TenantID == 1));
            return View(maintenanceorders.ToList());
        }

        // GET: /MaintenanceOrder/Details/5
        public ViewResult Details(int id)
        {
            var maintenanceorder = _db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        // GET: /MaintenanceOrder/Create
        public ActionResult Create()
        {
            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1");
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1");
            // ViewBag.TenantUserName = "Jack";
            return View();
        }

        // POST: /MaintenanceOrder/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        {
            //You can pass the entire model
            //TempData["MaintenanceOrderModel"] = maintenanceorder;

            if (ModelState.IsValid)
            {

                _db.MaintenanceOrders.Add(maintenanceorder);

                _db.SaveChanges();
                TempData["TenantUsername"] = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                TempData["RequestID"] = maintenanceorder.MaintenanceID;
                return RedirectToAction("Index", "Upload");
            }

            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        // GET: /MaintenanceOrder/Edit/5
        public ActionResult Edit(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            ViewBag.ServiceTypeID = new SelectList(_db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
            ViewBag.UrgencyID = new SelectList(_db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
            return View(maintenanceorder);
        }

        // POST: /MaintenanceOrder/Edit/5
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

        // GET: /MaintenanceOrder/Delete/5
        public ActionResult Delete(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            return View(maintenanceorder);
        }

        // POST: /MaintenanceOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceOrder maintenanceorder = _db.MaintenanceOrders.Find(id);
            _db.MaintenanceOrders.Remove(maintenanceorder);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddMorePhotos([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        {
            TempData["TenantUsername"] = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            TempData["RequestID"] = maintenanceorder.MaintenanceID;
            return RedirectToAction("Index", "Upload");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}