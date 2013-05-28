using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class AgentsController : Controller
    {
        private DB_33736_rentalEntities db = new DB_33736_rentalEntities();


        public ViewResult Index(int id)
        {
            var agent = db.Agents.Find(UserHelper.GetAgentID(id));
            ViewBag.agentProfile = agent;
            ViewBag.agentId = agent.AgentId;
            ViewBag.agentGoogleMap = agent.GoogleMap;
            return View(agent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
