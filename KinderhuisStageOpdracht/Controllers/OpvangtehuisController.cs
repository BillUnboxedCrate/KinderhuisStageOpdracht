using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{
    public class OpvangtehuisController : Controller
    {
        private readonly IOpvangtehuisRepository _opvangtehuisRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public OpvangtehuisController(IOpvangtehuisRepository opvangtehuisRepository, IGebruikerRepository gebruikerRepository)
        {
            _opvangtehuisRepository = opvangtehuisRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Opvangtehuis
        public ActionResult Suggesties()
        {
            //Opvangtehuis opvangtehuis = _gebruikerRepository.FindById((int) Session["gebruiker"]).Opvangtehuis;
            //var slvm = new OpvangtehuisViewModel.SuggestieListViewModel();

            //foreach (var s in opvangtehuis.Suggesties)
            //{
            //    var svm = new OpvangtehuisViewModel.SuggestieViewModel
            //    {
            //        Client = s.ToString(),
            //        Id = s.Id,
            //        Genre = s.Genre,
            //        TimeStamp = s.TimeStamp
            //    };
            //    slvm.Suggesties.Add(svm);
            //}
            //return View(slvm);
            return View();
        }

        public ActionResult CreateSuggestie()
        {
            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);
        }

        [HttpPost]
        public ActionResult CreateSuggestie(OpvangtehuisViewModel.CreateSuggestieViewModel model)
        {
            Suggestie s = new Suggestie()
            {
                Beschrijving = model.Beschrijving,
                Genre = model.GeselecteerdGenre,
                Client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]),
                TimeStamp = DateTime.Now
            };

            Opvangtehuis opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            opvangtehuis.AddSuggestie(s);
            _opvangtehuisRepository.SaveChanges();

            return View();
        }
    }
}