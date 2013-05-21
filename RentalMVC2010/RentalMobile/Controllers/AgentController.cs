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
    public class AgentController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();

        //
        // GET: /Agent/

        public ViewResult Index()
        {
            return View(db.Agents.ToList());
        }

        //
        // GET: /Agent/Details/5

        public ViewResult Details(int id)
        {
            Agent agent = db.Agents.Find(id);
            return View(agent);
        }

        //
        // GET: /Agent/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Agent/Create

        [HttpPost]
        public ActionResult Create(Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(agent);
        }
        
        //
        // GET: /Agent/Edit/5
 
        public ActionResult Edit(int id)
        {
            Agent agent = db.Agents.Find(id);
            return View(agent);
        }

        //
        // POST: /Agent/Edit/5

        [HttpPost]
        public ActionResult Edit(Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        //
        // GET: /Agent/Delete/5
 
        public ActionResult Delete(int id)
        {
            Agent agent = db.Agents.Find(id);
            return View(agent);
        }

        //
        // POST: /Agent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Agent agent = db.Agents.Find(id);
            db.Agents.Remove(agent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}