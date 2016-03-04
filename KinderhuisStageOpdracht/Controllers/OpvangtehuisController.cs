using System;
using System.Collections.Generic;
using System.Globalization;
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

            foreach (var s in opvangtehuis.GetSuggesties())
            {
                var svm = new OpvangtehuisViewModel.SuggestieViewModel(s.TimeStamp, s.Genre, s.Client.GiveFullName(),
                    s.Beschrijving, s.Id);
                slvm.AddSuggestie(svm);
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
                try
                {
                    var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                    opvangtehuis.AddSuggestie(model.Beschrijving, model.GeselecteerdGenre,
                        (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]));
                    _opvangtehuisRepository.SaveChanges();

                    this.AddNotification("Uw suggestie wordt doorgegeven", NotificationType.SUCCESS);
                    return RedirectToAction("ClientIndex", "Gebruiker");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);

        }

        public ActionResult DeleteSuggestie(int id)
        {
            var s = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis.FindSuggestieById(id);
            var svm = new OpvangtehuisViewModel.SuggestieViewModel(s.TimeStamp, s.Genre, s.Client.GiveFullName(), s.Beschrijving, s.Id);

            return View(svm);
        }

        [HttpPost, ActionName("DeleteSuggestie")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSuggestion(int id)
        {
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.DeleteSuggestie(id);
                _opvangtehuisRepository.SaveChanges();

                this.AddNotification("De suggestie is verwijderd", NotificationType.SUCCESS);
                return RedirectToAction("Suggesties");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        public ActionResult MenuIndex()
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var mlvm = new OpvangtehuisViewModel.MenuListViewModel();

            foreach (var m in opvangtehuis.GetMenus())
            {
                var mvm = new OpvangtehuisViewModel.MenuViewModel(m.Id, m.Week, m.BegindagWeek, m.EinddagWeek);

                mlvm.AddMenu(mvm);
            }

            return View(mlvm);
        }

        public ActionResult CreateMenu()
        {
            var mvm = new OpvangtehuisViewModel.MenuViewModel();
            return View(mvm);
        }

        //[HttpPost]
        //public ActionResult CreateMenu(OpvangtehuisViewModel.MenuViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var menu = new Menu
        //        {
        //            BegindagWeek = model.BeginWeek,
        //            EinddagWeek = model.EindeWeek,
        //            Week = model.Week
        //        };

        //        foreach (var mi in model.MenuItemListViewModels)
        //        {
        //            menu.AddMenuItem(mi.Dessert, mi.Hoofdgerecht, mi.Voorgerecht);
        //        }

        //        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
        //        opvangtehuis.AddMenu(menu);
        //        _opvangtehuisRepository.SaveChanges();

        //        return RedirectToAction("MenuIndex");
        //    }

        //    return View();
        //}

        //Extreem slordige code, moet later herwerkt worden
        [HttpPost]
        public ActionResult CreateMenu(OpvangtehuisViewModel.MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id <= 0)
                    {
                        var menu = new Menu(model.BeginWeek);

                        menu.AddMenuItem("Maandag", model.MaandagViewModel.Hoofdgerecht, model.MaandagViewModel.Voorgerecht,
                            model.MaandagViewModel.Dessert);
                        menu.AddMenuItem("Dinsdag", model.DinsdagViewModel.Hoofdgerecht, model.DinsdagViewModel.Voorgerecht,
                            model.DinsdagViewModel.Dessert);
                        menu.AddMenuItem("Woensdag", model.WoensdagViewModel.Hoofdgerecht,
                            model.WoensdagViewModel.Voorgerecht, model.WoensdagViewModel.Dessert);
                        menu.AddMenuItem("Donderdag", model.DonderdagViewModel.Hoofdgerecht,
                            model.DonderdagViewModel.Voorgerecht, model.DonderdagViewModel.Dessert);
                        menu.AddMenuItem("Vrijdag", model.VrijdagViewModel.Hoofdgerecht, model.VrijdagViewModel.Voorgerecht,
                            model.VrijdagViewModel.Dessert);

                        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                        opvangtehuis.AddMenu(menu);

                        this.AddNotification("De menu is aangemaakt", NotificationType.SUCCESS);
                    }
                    else
                    {
                        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;

                        var menu = opvangtehuis.Menus.FirstOrDefault(m => m.Id == model.Id);

                        menu.AanpassenBeginDatum(model.BeginWeek);

                        //Maandag
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Hoofdgerecht = model.MaandagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Voorgerecht = model.MaandagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dessert = model.MaandagViewModel.Hoofdgerecht;

                        //Dinsdag
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Hoofdgerecht = model.DinsdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Voorgerecht = model.DinsdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dessert = model.DinsdagViewModel.Hoofdgerecht;


                        //Woensdag
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Hoofdgerecht = model.WoensdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Voorgerecht = model.WoensdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dessert = model.WoensdagViewModel.Hoofdgerecht;


                        //Donderdag
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Hoofdgerecht = model.DonderdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Voorgerecht = model.DonderdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dessert = model.DonderdagViewModel.Hoofdgerecht;

                        //Vrijdag
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Hoofdgerecht = model.VrijdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Voorgerecht = model.VrijdagViewModel.Hoofdgerecht;
                        menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dessert = model.VrijdagViewModel.Hoofdgerecht;

                        this.AddNotification("De menu is gewijzigd", NotificationType.SUCCESS);

                    }
                    _opvangtehuisRepository.SaveChanges();
                    return RedirectToAction("MenuIndex");
                }
                catch (ApplicationException e)
                {

                    ModelState.AddModelError("", e.Message);
                }
            }
            this.AddNotification("Er was ergens een fout", NotificationType.ERROR);
            return View();
        }

        //Extreem slordige code, moet later herwerkt worden
        public ActionResult EditMenu(int id)
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;

            var menu = opvangtehuis.Menus.FirstOrDefault(m => m.Id == id);

            var mvm = new OpvangtehuisViewModel.MenuViewModel
            {
                Id = id,
                BeginWeek = menu.BegindagWeek,
                Week = menu.Week,
                MaandagViewModel = new OpvangtehuisViewModel.CreateMenuItemMaandagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dessert
                },
                DinsdagViewModel = new OpvangtehuisViewModel.CreateMenuItemDinsdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dessert
                },
                WoensdagViewModel = new OpvangtehuisViewModel.CreateMenuItemWoensdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dessert
                },
                DonderdagViewModel = new OpvangtehuisViewModel.CreateMenuItemDonderdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dessert
                },
                VrijdagViewModel = new OpvangtehuisViewModel.CreateMenuItemVrijdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dessert
                }
            };

            return View("CreateMenu", mvm);
        }

        //Reformat needed
        public ActionResult WeekMenu()
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;

            var menu = opvangtehuis.Menus.FirstOrDefault(m => m.Week == GetWeekVanHetJaar(DateTime.Today));

            var mvm = new OpvangtehuisViewModel.MenuViewModel
            {
                BeginWeek = menu.BegindagWeek,
                Week = menu.Week,
                MaandagViewModel = new OpvangtehuisViewModel.CreateMenuItemMaandagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dessert
                },
                DinsdagViewModel = new OpvangtehuisViewModel.CreateMenuItemDinsdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dessert
                },
                WoensdagViewModel = new OpvangtehuisViewModel.CreateMenuItemWoensdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dessert
                },
                DonderdagViewModel = new OpvangtehuisViewModel.CreateMenuItemDonderdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dessert
                },
                VrijdagViewModel = new OpvangtehuisViewModel.CreateMenuItemVrijdagViewModel()
                {
                    Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dag,
                    Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Hoofdgerecht,
                    Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Voorgerecht,
                    Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dessert
                }
            };

            return View(mvm);
        }

        private int GetWeekVanHetJaar(DateTime datum)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = dfi.Calendar;

            return cal.GetWeekOfYear(datum, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}