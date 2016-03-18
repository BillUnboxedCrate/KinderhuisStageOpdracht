using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderhuisStageOpdracht.Controllers
{
    public class PlanningController : Controller
    {
        // GET: Planning
        public ActionResult Index()
        {
            return View();
        }


        #region helpers
        public ActionResult UserStillLoggedIn()
        {
            if (Session["gebruiker"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return null;
        }
        #endregion
    }
}