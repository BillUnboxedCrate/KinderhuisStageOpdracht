using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{
    public class StrafController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public StrafController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Klacht
        public ActionResult Index()
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var silvm = new StrafViewModel.StrafListIndexViewModel();
            foreach (var s in opvangtehuis.GetStraffen())
            {
                silvm.AddStrafIndexViewModel(new StrafViewModel.StrafIndexViewModel(s.Id, s.Naam));
            }
            return View(silvm);
        }

        public ActionResult CreateStraf()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStraf(StrafViewModel.StrafIndexViewModel model)
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            if (ModelState.IsValid)
            {
                opvangtehuis.AddStraf(new Straf(model.Naam, "~/Content/Images/Aanduidingen/vraagteken.png"));
                _gebruikerRepository.SaveChanges();

                this.AddNotification("Straf toegevoegd", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View();
            
        }
    }
}