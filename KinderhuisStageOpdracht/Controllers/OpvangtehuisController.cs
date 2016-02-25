using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Extensions;
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
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var slvm = new OpvangtehuisViewModel.SuggestieListViewModel();

            foreach (var s in opvangtehuis.Suggesties.OrderByDescending(s => s.TimeStamp))
            {
                var svm = new OpvangtehuisViewModel.SuggestieViewModel
                {
                    Client = s.Client.GiveFullName(),
                    Id = s.Id,
                    Genre = s.Genre,
                    TimeStamp = s.TimeStamp,
                    Beschrijving = s.Beschrijving
                };
                slvm.Suggesties.Add(svm);
            }
            return View(slvm);
        }

        public ActionResult CreateSuggestie()
        {
            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);
        }

        [HttpPost]
        public ActionResult CreateSuggestie(OpvangtehuisViewModel.CreateSuggestieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var s = new Suggestie()
                {
                    Beschrijving = model.Beschrijving,
                    Genre = model.GeselecteerdGenre,
                    Client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]),
                    TimeStamp = DateTime.Now
                };

                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.AddSuggestie(s);
                _opvangtehuisRepository.SaveChanges();

                this.AddNotification("Uw suggestie wordt doorgegeven", NotificationType.SUCCESS);
                return RedirectToAction("ClientIndex", "Gebruiker");
            }

            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);
           
        }

        public ActionResult MenuIndex()
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var mlvm = new OpvangtehuisViewModel.MenuListViewModel();

            foreach (var m in opvangtehuis.Menus.OrderByDescending(m => m.Week))
            {
                var mvm = new OpvangtehuisViewModel.MenuViewModel
                {
                    Week = m.Week,
                    BeginWeek = m.BegindagWeek,
                    EindeWeek = m.EinddagWeek
                };
                mlvm.Menus.Add(mvm);
            }

            return View(mlvm);
        }

        public ActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMenu(OpvangtehuisViewModel.MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var menu = new Menu
                {
                    BegindagWeek = model.BeginWeek,
                    EinddagWeek = model.EindeWeek,
                    Week = model.Week
                };

                foreach (var mi in model.MenuItemListViewModels)
                {
                    menu.AddMenuItem(mi.Dessert, mi.Hoofdgerecht, mi.Voorgerecht);
                }

                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.AddMenu(menu);
                _opvangtehuisRepository.SaveChanges();

                return RedirectToAction("MenuIndex");
            }

            return View();
        }
    }
}