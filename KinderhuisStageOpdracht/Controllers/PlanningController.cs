using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KinderhuisStageOpdracht.App_Start;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{
    public class PlanningController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public PlanningController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Planning
        public ActionResult ClientPlanning()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);


            var cplvm = new PlanningViewModel.ClientPlanningListViewModel();

            foreach (var i in client.GetPlanning())
            {
                cplvm.AddItem(new PlanningViewModel.ClientPlanningViewModel(i.Id, i.Actie, i.Datum));
            }

            return View(cplvm);
        }

        [HttpPost]
        public ActionResult ClientPlanning(PlanningViewModel.ClientPlanningListViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);

            client.AddPlanning(model.ClientPlanningViewModel.Datum, model.ClientPlanningViewModel.Activiteit);

            _gebruikerRepository.SaveChanges();

            var cplvm = new PlanningViewModel.ClientPlanningListViewModel();

            foreach (var i in client.GetPlanning())
            {
                cplvm.AddItem(new PlanningViewModel.ClientPlanningViewModel(i.Id, i.Actie, i.Datum));
            }

            return View(cplvm);
        }

        public ActionResult RemoveClientPlanningItem(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            try
            {
                var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
                client.RemovePlanning(id);
                _gebruikerRepository.SaveChanges();

                return RedirectToAction("ClientPlanning");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return RedirectToAction("ClientPlanning");
        }




        #region helpers
        public bool UserStillLoggedIn()
        {
            return Session["gebruiker"] == null;
        }

        public ActionResult ReturnToLogin()
        {
            FormsAuthentication.SignOut();
            Session["gebruiker"] = null;
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}