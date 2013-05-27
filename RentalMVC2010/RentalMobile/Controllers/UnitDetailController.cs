using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class UnitDetailController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /UnitDetail/

        public ActionResult Index(int id )
        {
            Unit unit = db.Units.Find(id) ?? db.Units.Find(1);
            ViewBag.UnitId = unit.UnitId;
            ViewBag.UnitGoogleMap = unit.GoogleMap;
            return View(unit);
        }


        public ActionResult JsonFun(int id)
        {
            var data = new List<UnitGalleryJsonData>();
            IQueryable<UnitGallery> unitGallery = db.UnitGalleries.Where(x => x.UnitId == id);
            if (unitGallery.Count() != 0)
            {
                data.AddRange(unitGallery.Select(photo => new UnitGalleryJsonData {href = photo.Path, title = photo.Caption}));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }



    public class UnitGalleryJsonData
    {
        public string href { get; set; }

        public string title { get; set; }
    }


}
