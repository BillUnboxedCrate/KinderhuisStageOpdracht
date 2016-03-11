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
        private readonly IStrafRepository _strafRepository;

        public StrafController(IStrafRepository strafRepository)
        {
            _strafRepository = strafRepository;
        }

        // GET: Klacht
        public ActionResult Index()
        {
            var silvm = new StrafViewModel.StrafListIndexViewModel();
            foreach (var s in _strafRepository.FindAll())
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
            if (ModelState.IsValid)
            {
                _strafRepository.AddStraf(new Straf(model.Naam, "~/Content/Images/Aanduidingen/vraagteken.png"));
                _strafRepository.SaveChanges();

                this.AddNotification("Straf toegevoegd", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View();
            
        }
    }
}