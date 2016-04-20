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


            var plvm = new PlanningViewModel.PlanningListViewModel();

            foreach (var i in client.GetPlanning())
            {
                plvm.AddItem(new PlanningViewModel.PlanningItemViewModel(i.Id, i.Actie, i.Datum));
            }

            return View(plvm);
        }

        [HttpPost]
        public ActionResult ClientPlanning(PlanningViewModel.PlanningListViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);

            client.AddPlanning(model.ClientPlanningViewModel.Datum, model.ClientPlanningViewModel.Activiteit);

            _gebruikerRepository.SaveChanges();

            var plvm = new PlanningViewModel.PlanningListViewModel();

            foreach (var i in client.GetPlanning())
            {
                plvm.AddItem(new PlanningViewModel.PlanningItemViewModel(i.Id, i.Actie, i.Datum));
            }

            return View(plvm);
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

                return RedirectToAction("Planning");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return RedirectToAction("Planning");
        }

        public ActionResult PlanningOverview(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById(id);


            var plvm = new PlanningViewModel.PlanningListViewModel();

            foreach (var i in client.GetPlanning())
            {
                plvm.AddItem(new PlanningViewModel.PlanningItemViewModel(i.Id, i.Actie, i.Datum));
            }

            return View(plvm);
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