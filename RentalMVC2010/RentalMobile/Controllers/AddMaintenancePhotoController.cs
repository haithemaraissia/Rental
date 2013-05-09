using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{

    [Authorize]
    public class AddMaintenancePhotoController : Controller
    {

        //Variables that should be queried with the request
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        public string TenantUsername = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).ToString();
        public string TenantPhotoPath = @"~/Photo/Tenant/Requests";
        public string RequestID;

        //
        // GET: /Upload/

        public ActionResult Index(int Id)
        {
            if (db.MaintenanceOrders.Find(Id) == null)
            {
                RedirectToAction("Index", "MaintenanceOrder");
            }
            var temp = db.MaintenanceOrders.Find(Id);
            RequestID = Id.ToString(CultureInfo.InvariantCulture);
            // RequestID = TempData["RequestID"].ToString();
            //   ViewBag.TenantUserName = TenantUsername;

            ViewBag.TenantUserName = TenantUsername;
            ViewBag.RequestID = RequestID;
            TempData["RequestID"] = RequestID;
            return View();
        }









        //
        // POST: /MaintenanceOrder/Create

        //[HttpPost]
        //public ActionResult Create([Bind(Exclude = "MaintenanceID")]MaintenanceOrder maintenanceorder)
        //{



        //    HttpPostedFileBase fileData = Request.Files[0];

        //    if (fileData == null)
        //    {
        //        fileData = Request.Files[0];

        //        if (fileData.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(fileData.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Content"), fileName);
        //            fileData.SaveAs(path);
        //        }

        //    }


        //    if (ModelState.IsValid)
        //    {
        //        db.MaintenanceOrders.Add(maintenanceorder);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "ServiceType1", maintenanceorder.ServiceTypeID);
        //    ViewBag.UrgencyID = new SelectList(db.UrgencyTypes, "UrgencyTypeID", "UrgencyType1", maintenanceorder.UrgencyID);
        //    return View(maintenanceorder);
        //}




        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            SavePictures();
            return RedirectToAction("Index", "MaintenanceOrder");
        }


        public void SavePictures()
        {
            var imageStoragePath = Server.MapPath("~/UploadedImages");
            var photoPath = Server.MapPath(TenantPhotoPath);
            var directory = @"\" + TenantUsername + @"\" + "Requests" + @"\" + TempData["RequestID"] + @"\";
            var path = imageStoragePath + directory;
            var uploadDirectory = new DirectoryInfo(path);
            var newdirectory = photoPath + directory;
            if (Directory.Exists(path))
            {

                CreateDirectoryIfNotExist(newdirectory);
            }

            var files = uploadDirectory.GetFiles();

            foreach (var f in files)
            {

                //This is what you need.
                var destinationFile = newdirectory + @"\" + f.Name;
                var virtualdestinationFile = @"~\Photo\Tenant\Requests" + directory + f.Name;
                if (!System.IO.File.Exists(destinationFile))
                {
                    System.IO.File.Move(f.FullName, destinationFile);
                    AddPicture(Convert.ToInt32(TempData["RequestID"]), virtualdestinationFile);
                }
                if (System.IO.File.Exists(f.Name))
                    System.IO.File.Delete(f.Name);
            }
            DeleteDirectoryIfExist(path);
        }

        public void AddPicture(int maintenanceId, string photoPath)
        {
            var maintenancephoto = new MaintenancePhoto { MaintenanceID = maintenanceId, PhotoPath = photoPath };
            if (!ModelState.IsValid) return;
            db.MaintenancePhotoes.Add(maintenancephoto);
            db.SaveChanges();
        }

        /// <summary>
        /// Addition of Helper function to create and/or delete directory
        /// </summary>
        /// <param name="newDirectory"></param>
        private void CreateDirectoryIfNotExist(string newDirectory)
        {
            try
            {
                // Checking the existence of directory
                if (!Directory.Exists(newDirectory))
                {

                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(newDirectory);
                }
            }
            catch (IOException err)
            {
                Response.Write(err.Message);
            }
        }
        private void DeleteDirectoryIfExist(string newDirectory)
        {
            try
            {
                // Checking the existence of directory
                if (Directory.Exists(newDirectory))
                {

                    string[] files = Directory.GetFiles(newDirectory);
                    foreach (string file in files)
                        System.IO.File.Delete(file);

                    Directory.Delete(newDirectory);
                }
            }
            catch (IOException err)
            {
                Response.Write(err.Message);
            }
        }
    }
}
