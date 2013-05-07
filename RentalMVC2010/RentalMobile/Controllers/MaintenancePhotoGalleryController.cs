using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class MaintenancePhotoGalleryController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        public JsonResult GetJsonData()
        {
            //var persons = new List<Person>
            //                  {
            //                      new Person{Id = 1, FirstName = "F1", 
            //                          LastName = "L1", 
            //                          Addresses = new List<Address>
            //                                          {
            //                                              new Address{Line1 = "LaneA"},
            //                                              new Address{Line1 = "LaneB"}
            //                                          }},

            //                      new Person{Id = 2, FirstName = "F2", 
            //                          LastName = "L2", 
            //                          Addresses = new List<Address>
            //                                          {
            //                                              new Address{Line1 = "LaneC"},
            //                                              new Address{Line1 = "LaneD"}
            //                                          }}};

            var persons = db.MaintenancePhotoes.ToList();
            var p = persons.Select(d => new Photo {PhotoID = d.PhotoID, PathPath = d.PhotoPath}).ToList();



            for (int i = 0; i < p.Count; i ++ )
            {
              p[i].PathPath =  p[i].PathPath.Replace(oldValue: @"~\Photo", newValue: @"../../Photo").Replace("\\","/");
                  
            }
                //foreach (var i in p)
                //{
                //    i.PathPath.Replace(oldValue: @"~\Photo", newValue: @"../../Photo");
                //    i.PathPath.Replace(oldValue: "Photo", newValue: "image");
                //}
            return Json( p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateDetails(int id)
        {
            var a = db.MaintenancePhotoes.ToList();
            return Json(a, JsonRequestBehavior.AllowGet);
        }


        public ViewResult Json()
        {
            return View();
        }

        //public JsonResult PopulateDetails(int id)
        //{
        //    //userResultModel.LastName = user.LastName;
        //    //userResultModel.FirstName = user.FirstName;
        //    //userResultModel.Message = String.Empty; //success message is empty in this case

        //    MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(35);
        //   // IQueryable<MaintenancePhoto> maintenancephoto = db.MaintenancePhotoes.Where(o => o.MaintenanceID == 35);
        //    return Json(maintenancephoto, JsonRequestBehavior.AllowGet);
        //}



        //
        // GET: /MaintenancePhotoGallery/












        public ViewResult Index()
        {
            var maintenancephotoes = db.MaintenancePhotoes.Include(m => m.MaintenanceOrder);
            return View(maintenancephotoes.ToList());
        }

        //
        // GET: /MaintenancePhotoGallery/Details/5

        public ViewResult Details(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            return View(maintenancephoto);
        }

        //
        // GET: /MaintenancePhotoGallery/Create

        public ActionResult Create()
        {
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description");
            return View();
        }

        //
        // POST: /MaintenancePhotoGallery/Create

        [HttpPost]
        public ActionResult Create(MaintenancePhoto maintenancephoto)
        {
            if (ModelState.IsValid)
            {
                db.MaintenancePhotoes.Add(maintenancephoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description",
                                                   maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }

        //
        // GET: /MaintenancePhotoGallery/Edit/5

        public ActionResult Edit(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description",
                                                   maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }

        //
        // POST: /MaintenancePhotoGallery/Edit/5

        [HttpPost]
        public ActionResult Edit(MaintenancePhoto maintenancephoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenancephoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaintenanceID = new SelectList(db.MaintenanceOrders, "MaintenanceID", "Description",
                                                   maintenancephoto.MaintenanceID);
            return View(maintenancephoto);
        }

        //
        // GET: /MaintenancePhotoGallery/Delete/5

        public ActionResult Delete(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            return View(maintenancephoto);
        }

        //
        // POST: /MaintenancePhotoGallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenancePhoto maintenancephoto = db.MaintenancePhotoes.Find(id);
            db.MaintenancePhotoes.Remove(maintenancephoto);
            db.SaveChanges();
            return RedirectToAction("Index", "MaintenanceOrder");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }






























    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class Photo
    {
        public int PhotoID { get; set; }
        public string PathPath { get; set; }
    }
}
