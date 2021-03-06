﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;
using Microsoft.Ajax.Utilities;

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
        //public ActionResult Suggesties()
        //{
        //    if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
        //    {
        //        return ReturnToLogin();
        //    }

        //    if (!Request.IsAuthenticated)
        //    {
        //        return View("Error");
        //    }

        //    var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
        //    var slvm = new OpvangtehuisViewModel.SuggestieListViewModel();

        //    foreach (var s in opvangtehuis.GetSuggesties())
        //    {
        //        var svm = new OpvangtehuisViewModel.SuggestieViewModel(s.TimeStamp, s.Genre, s.Client.GiveFullName(),
        //            s.Beschrijving, s.Id);
        //        slvm.AddSuggestie(svm);
        //    }
        //    return View(slvm);
        //}

        public ActionResult Suggesties(string sortingOrder)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var slvm = new OpvangtehuisViewModel.SuggestieListViewModel();

            foreach (var s in opvangtehuis.GetSuggesties())
            {
                var svm = new OpvangtehuisViewModel.SuggestieViewModel(s.TimeStamp, s.Genre, s.Client.GiveFullName(),
                    s.Beschrijving, s.Id);
                slvm.AddSuggestie(svm);
            }

            switch (sortingOrder)
            {
                case "Genre":
                    slvm.Suggesties = slvm.Suggesties.OrderBy(m => m.Genre).ToList();
                    break;
                case "Client":
                    slvm.Suggesties = slvm.Suggesties.OrderBy(m => m.Client).ToList(); ;
                    break;
                case "Wanneer":
                    slvm.Suggesties = slvm.Suggesties.OrderByDescending(m => m.TimeStamp).ToList(); ;
                    break;
            }

            return View(slvm);
        }


        public ActionResult CreateSuggestie()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSuggestie(OpvangtehuisViewModel.CreateSuggestieViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                    opvangtehuis.AddSuggestie(model.Beschrijving, model.GeselecteerdGenre,
                        (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]));
                    _opvangtehuisRepository.SaveChanges();

                    this.AddNotification("Je suggestie wordt doorgegeven", NotificationType.SUCCESS);
                    return RedirectToAction("ClientIndex", "Gebruiker");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View("Error");
                }
            }

            var csvm = new OpvangtehuisViewModel.CreateSuggestieViewModel();
            return View(csvm);

        }

        //public ActionResult DeleteSuggestie(int id)
        //{
        //    if (UserStillLoggedIn() != null)
        //    {
        //        return UserStillLoggedIn();
        //    }

        //    if (!Request.IsAuthenticated)
        //    {
        //        return View("Error");
        //    }

        //    var s = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis.FindSuggestieById(id);
        //    var svm = new OpvangtehuisViewModel.SuggestieViewModel(s.TimeStamp, s.Genre, s.Client.GiveFullName(), s.Beschrijving, s.Id);

        //    return View(svm);
        //}

        //[HttpPost, ActionName("DeleteSuggestie")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteSuggestion(int id)
        //{
        //    if (UserStillLoggedIn() != null)
        //    {
        //        return UserStillLoggedIn();
        //    }
        //    try
        //    {
        //        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
        //        opvangtehuis.DeleteSuggestie(id);
        //        _opvangtehuisRepository.SaveChanges();

        //        this.AddNotification("De suggestie is verwijderd", NotificationType.SUCCESS);
        //        return RedirectToAction("Suggesties");
        //    }
        //    catch (ApplicationException e)
        //    {
        //        ModelState.AddModelError("", e.Message);
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult DeleteSuggestion(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.DeleteSuggestie(id);
                _opvangtehuisRepository.SaveChanges();

                return RedirectToAction("Suggesties");

            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex", "Gebruiker");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }
            return RedirectToAction("Suggesties");
        }

        public ActionResult MenuIndex()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var mlvm = new OpvangtehuisViewModel.MenuListViewModel();

            foreach (var m in opvangtehuis.GetMenus())
            {
                var mvm = new OpvangtehuisViewModel.MenuViewModel(m.Id, m.Week, m.BegindagWeek, m.EinddagWeek);

                mlvm.AddMenu(mvm);
            }

            return View(mlvm);
        }

        [HttpPost]
        public ActionResult DeleteMenu(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.DeleteMenu(id);
                _opvangtehuisRepository.SaveChanges();

                return RedirectToAction("Suggesties");

            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }

            return RedirectToAction("Suggesties");
        }

        public ActionResult CreateMenu()
        {
            if (UserStillLoggedIn())
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

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
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenu(OpvangtehuisViewModel.MenuViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            if (model.BeginWeek.DayOfWeek != DayOfWeek.Monday)
            {
                ModelState.AddModelError("", "De dag die gekozen is, is geen maandag");
                this.AddNotification("De dag die gekozen is, is geen maandag", NotificationType.ERROR);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id <= 0)
                    {


                        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                        if (opvangtehuis.FindMenuByDate(model.BeginWeek) == null)
                        {
                            var menu = new Menu(model.BeginWeek);

                            if (model.MenuImageUpload != null)
                            {
                                if (ImageIsValidType(model.MenuImageUpload))
                                {
                                    menu.ImageUrl = ImageUploadMenuAfbeelding(model.MenuImageUpload);
                                    opvangtehuis.AddMenu(menu);
                                    _opvangtehuisRepository.SaveChanges();
                                    this.AddNotification("De menu is aangemaakt", NotificationType.SUCCESS);
                                }
                                else
                                {
                                    this.AddNotification("Dit is geen foto", NotificationType.ERROR);
                                    ModelState.AddModelError("MenuImageUpload", "Dit is geen foto");
                                }

                            }

                            menu.AddMenuItem("Maandag", model.MaandagViewModel.Hoofdgerecht,
                                model.MaandagViewModel.Voorgerecht,
                                model.MaandagViewModel.Dessert);
                            menu.AddMenuItem("Dinsdag", model.DinsdagViewModel.Hoofdgerecht,
                                model.DinsdagViewModel.Voorgerecht,
                                model.DinsdagViewModel.Dessert);
                            menu.AddMenuItem("Woensdag", model.WoensdagViewModel.Hoofdgerecht,
                                model.WoensdagViewModel.Voorgerecht, model.WoensdagViewModel.Dessert);
                            menu.AddMenuItem("Donderdag", model.DonderdagViewModel.Hoofdgerecht,
                                model.DonderdagViewModel.Voorgerecht, model.DonderdagViewModel.Dessert);
                            menu.AddMenuItem("Vrijdag", model.VrijdagViewModel.Hoofdgerecht,
                                model.VrijdagViewModel.Voorgerecht,
                                model.VrijdagViewModel.Dessert);
                            menu.AddMenuItem("Zaterdag", model.VrijdagViewModel.Hoofdgerecht,
                                model.VrijdagViewModel.Voorgerecht,
                                model.VrijdagViewModel.Dessert);
                            menu.AddMenuItem("Zondag", model.VrijdagViewModel.Hoofdgerecht,
                                model.VrijdagViewModel.Voorgerecht,
                                model.VrijdagViewModel.Dessert);

                            //var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                            opvangtehuis.AddMenu(menu);

                            this.AddNotification("De menu is aangemaakt", NotificationType.SUCCESS);
                        }
                        else
                        {
                            this.AddNotification("Er is al reeds een menu op deze dag", NotificationType.ERROR);
                            return RedirectToAction("CreateMenu");
                        }
                    }
                    else
                    {
                        var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                        if (opvangtehuis.FindMenuByDate(model.BeginWeek) == null)
                        {
                            var menu = opvangtehuis.Menus.FirstOrDefault(m => m.Id == model.Id);

                            menu.AanpassenBeginDatum(model.BeginWeek);

                            if (model.MenuImageUpload != null)
                            {
                                if (ImageIsValidType(model.MenuImageUpload))
                                {
                                    menu.ImageUrl = ImageUploadMenuAfbeelding(model.MenuImageUpload);
                                    opvangtehuis.AddMenu(menu);
                                    _opvangtehuisRepository.SaveChanges();
                                    this.AddNotification("De menu is aangemaakt", NotificationType.SUCCESS);

                                }
                                else
                                {
                                    ModelState.AddModelError("MenuImageUpload", "Dit is geen foto");
                                }

                            }

                            //Maandag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Voorgerecht = model.MaandagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Hoofdgerecht = model.MaandagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Maandag").Dessert = model.MaandagViewModel.Dessert;

                            //Dinsdag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Voorgerecht = model.DinsdagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Hoofdgerecht = model.DinsdagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Dinsdag").Dessert = model.DinsdagViewModel.Dessert;


                            //Woensdag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Voorgerecht = model.WoensdagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Hoofdgerecht = model.WoensdagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Woensdag").Dessert = model.WoensdagViewModel.Dessert;


                            //Donderdag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Voorgerecht = model.DonderdagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Hoofdgerecht = model.DonderdagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Donderdag").Dessert = model.DonderdagViewModel.Dessert;

                            //Vrijdag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Voorgerecht = model.VrijdagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Hoofdgerecht = model.VrijdagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Vrijdag").Dessert = model.VrijdagViewModel.Dessert;

                            //Zaterdag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Voorgerecht = model.ZaterdagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Hoofdgerecht = model.ZaterdagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Dessert = model.ZaterdagViewModel.Dessert;

                            //Zondag
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Voorgerecht = model.ZondagViewModel.Voorgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Hoofdgerecht = model.ZondagViewModel.Hoofdgerecht;
                            menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Dessert = model.ZondagViewModel.Dessert;

                            this.AddNotification("De menu is gewijzigd", NotificationType.SUCCESS);
                        }
                        else
                        {
                            this.AddNotification("Er is al reeds een menu op deze dag", NotificationType.ERROR);
                            return RedirectToAction("EditMenu", new {id = model.Id});
                        }
                    }
                    _opvangtehuisRepository.SaveChanges();
                    return RedirectToAction("MenuIndex");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View("Error");
                }
            }

            return RedirectToAction("CreateMenu");
        }

        //Extreem slordige code, moet later herwerkt worden
        public ActionResult EditMenu(int id)
        {
            try
            {
                if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
                {
                    return ReturnToLogin();
                }

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
                    },

                    ZaterdagViewModel = new OpvangtehuisViewModel.CreateMenuItemZaterdagViewModel()
                    {
                        Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Dag,
                        Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Hoofdgerecht,
                        Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Voorgerecht,
                        Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Dessert
                    },

                    ZondagViewModel = new OpvangtehuisViewModel.CreateMenuItemZondagViewModel()
                    {
                        Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Dag,
                        Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Hoofdgerecht,
                        Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Voorgerecht,
                        Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Dessert
                    }
                };

                return View("CreateMenu", mvm);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("OpvoederIndex", "Gebruiker");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }
        }

        //Reformat needed
        public ActionResult WeekMenu(int week)
        {
            if (UserStillLoggedIn() || (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
            }

            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                Menu menu;
                menu = week != 0 ? opvangtehuis.Menus.FirstOrDefault(m => m.Week == week) : opvangtehuis.Menus.FirstOrDefault(m => m.Week == GetWeekVanHetJaar(DateTime.Today));
                string typeGebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().FullName;

                if (menu == null)
                {
                    return View(new OpvangtehuisViewModel.MenuViewModel("Er is nog geen menu gemaakt deze week", typeGebruiker));
                }

                var mvm = new OpvangtehuisViewModel.MenuViewModel
                {
                    BeginWeek = menu.BegindagWeek,
                    Week = menu.Week,
                    TypeGebruiker = typeGebruiker,
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
                    },
                    ZaterdagViewModel = new OpvangtehuisViewModel.CreateMenuItemZaterdagViewModel()
                    {
                        Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Dag,
                        Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Hoofdgerecht,
                        Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Voorgerecht,
                        Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zaterdag").Dessert
                    },
                    ZondagViewModel = new OpvangtehuisViewModel.CreateMenuItemZondagViewModel()
                    {
                        Dag = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Dag,
                        Hoofdgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Hoofdgerecht,
                        Voorgerecht = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Voorgerecht,
                        Dessert = menu.MenuItems.FirstOrDefault(mi => mi.Dag == "Zondag").Dessert
                    }
                };
                return View(mvm);
            }
            catch (NullReferenceException ex)
            {
                return ReturnToLogin();
            }

            return View();
        }


        //public ActionResult KlachtIndex()
        //{
        //    if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
        //    {
        //        return ReturnToLogin();
        //    }

        //    var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
        //    var lkvm = new OpvangtehuisViewModel.ListKlachtViewModel();

        //    foreach (var klacht in opvangtehuis.GetKlachten())
        //    {
        //        lkvm.AddKlacht(new OpvangtehuisViewModel.KlachtViewModel(klacht.Id, klacht.Omschrijving, klacht.Client.GiveFullName(), klacht.TimeStamp));
        //    }

        //    return View(lkvm);
        //}

        public ActionResult KlachtIndex(string sortingOrder)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var lkvm = new OpvangtehuisViewModel.ListKlachtViewModel();

            foreach (var klacht in opvangtehuis.GetKlachten())
            {
                lkvm.AddKlacht(new OpvangtehuisViewModel.KlachtViewModel(klacht.Id, klacht.Omschrijving, klacht.Client.GiveFullName(), klacht.TimeStamp));
            }

            switch (sortingOrder)
            {
                case "Client":
                    lkvm.List = lkvm.List.OrderBy(m => m.Client).ToList();
                    break;
                case "Date":
                    lkvm.List = lkvm.List.OrderByDescending(m => m.TimeStamp).ToList(); ;
                    break;
            }

            return View(lkvm);
        }

        [HttpPost]
        public ActionResult DeleteKlacht(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.DeleteKlacht(id);
                _opvangtehuisRepository.SaveChanges();

                this.AddNotification("De klacht is verwijderd", NotificationType.SUCCESS);
                return RedirectToAction("KlachtIndex");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex", "Gebruiker");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }
            return RedirectToAction("KlachtIndex");
        }

        public ActionResult Klacht()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Klacht(OpvangtehuisViewModel.KlachtViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                    opvangtehuis.AddKlacht(model.Omschrijving,
                        (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]));

                    _opvangtehuisRepository.SaveChanges();

                    this.AddNotification("Je klacht wordt doorgegeven", NotificationType.SUCCESS);
                    return RedirectToAction("ClientIndex", "Gebruiker");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View("Error");
                }
            }
            return View();
        }

        public ActionResult KamerOpdracht()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var kol = new OpvangtehuisViewModel.KamerOpdrachtListViewModel();

            foreach (var opdracht in opvangtehuis.GetKamerControleOpdrachten())
            {
                kol.AddItem(new OpvangtehuisViewModel.KamerOprachtViewModel(opdracht.Id, opdracht.Titel, opdracht.ImageUrl));
            }

            return View(kol);
        }

        [HttpPost]
        public ActionResult KamerOpdracht(OpvangtehuisViewModel.KamerOpdrachtListViewModel model)
        {

            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            if (!ImageIsValidType(model.KamerOpracht.ImageUpload))
            {
                ModelState.AddModelError("ImageUpload", "Dit is geen foto");
            }
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                if (ModelState.IsValid)
                {
                    opvangtehuis.AddOpdracht(new KamerControleOpdracht(model.KamerOpracht.Titel,
                        ImageUploadKamerControleAfbeelding(model.KamerOpracht.ImageUpload)));
                    _opvangtehuisRepository.SaveChanges();

                    this.AddNotification("Straf toegevoegd", NotificationType.SUCCESS);
                }

                opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                var kol = new OpvangtehuisViewModel.KamerOpdrachtListViewModel();
                foreach (var opdracht in opvangtehuis.GetKamerControleOpdrachten())
                {
                    kol.AddItem(new OpvangtehuisViewModel.KamerOprachtViewModel(opdracht.Id, opdracht.Titel,
                        opdracht.ImageUrl));
                }
                return View(kol);
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex", "Gebruiker");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteKamerOpdracht(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }
            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.RemoveOpdracht(id);
                _opvangtehuisRepository.SaveChanges();

                this.AddNotification("De kamercontrole opdracht is verwijderd", NotificationType.SUCCESS);
                return RedirectToAction("KamerOpdracht");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex", "Gebruiker");
            }
            return RedirectToAction("KamerOpdracht");
        }


        #region helpers
        private int GetWeekVanHetJaar(DateTime datum)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = dfi.Calendar;

            return cal.GetWeekOfYear(datum, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public bool UserStillLoggedIn()
        {
            return Session["gebruiker"] == null;
        }

        public ActionResult ReturnToLogin()
        {
            FormsAuthentication.SignOut();
            Session["gebruiker"] = null;
            ViewBag.IsForcedLogout = true;
            return RedirectToAction("Login", "Account");
        }
        public bool ImageIsValidType(HttpPostedFileBase file)
        {
            var validImageTypes = new[]
            {
                "image/jpg",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (file == null || validImageTypes.Contains(file.ContentType))
            {
                return true;
            }
            return false;
        }

        public string ImageUploadMenuAfbeelding(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("/Content/Images/Menu"), pic);

                file.SaveAs(path);

                return "/Content/Images/Menu/" + pic;
            }
            return null;
        }

        public string ImageUploadKamerControleAfbeelding(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("/Content/Images/KamerControleImages"), pic);

                file.SaveAs(path);

                return "/Content/Images/KamerControleImages/" + pic;
            }
            return null;
        }
        #endregion
    }
}