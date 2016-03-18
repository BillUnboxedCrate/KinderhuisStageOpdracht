﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{

    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IOpvangtehuisRepository _opvangtehuisRepository;

        public GebruikerController(IGebruikerRepository gebruikerRepository, IOpvangtehuisRepository opvangtehuisRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            _opvangtehuisRepository = opvangtehuisRepository;
        }

        // GET: Gebruiker
        //[Authorize]
        public ActionResult ClientIndex()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (Session["gebruiker"] == null || !Request.IsAuthenticated)
            {
                return View("Error");
            }
            var id = (int)Session["gebruiker"];
            var client = (Client)_gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());

            var clientlistvm = new GebruikerViewModel.OpvoederListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().Where(c => c.Opvangtehuis.Id == client.Opvangtehuis.Id).ToList();

            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder)gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel(o.Voornaam, o.GiveFullName(), o.ImageUrl);
                clientlistvm.AddOpvoeder(opvoedervm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult OpvoederIndex()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (Session["gebruiker"] == null || !Request.IsAuthenticated)
            {
                return View("Error");
            }
            var id = (int)Session["gebruiker"];
            var opvoeder = (Opvoeder)_gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(opvoeder.GetType());

            var clientlistvm = new GebruikerViewModel.ClientListViewModel();

            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().Where(c => c.Opvangtehuis.Id == opvoeder.Opvangtehuis.Id).ToList();

            foreach (var gebruiker in clients)
            {
                var c = (Client)gebruiker;
                var clientvm = new GebruikerViewModel.ClientViewModel(c.GiveFullName(), c.Voornaam, c.ImageUrl);

                clientlistvm.AddClient(clientvm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult AdminIndex()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (Session["gebruiker"] == null || !Request.IsAuthenticated)
            {
                return View("Error");
            }
            var id = (int)Session["gebruiker"];
            //Session["gebruiker"] = id;
            var admin = (Admin)_gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(admin.GetType());

            var opvoederlistvm = new GebruikerViewModel.OpvoederListViewModel();
            var clientlistvm = new GebruikerViewModel.ClientListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().ToList();
            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().ToList();

            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder)gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel(o.Id, o.GiveFullName(), o.Email,
                    o.GetOpvangtehuisnaam());

                opvoederlistvm.AddOpvoeder(opvoedervm);
            }

            foreach (var gebruiker in clients)
            {
                var c = (Client)gebruiker;
                var clientvm = new GebruikerViewModel.ClientViewModel(c.Id, c.GiveFullName(), c.Email,
                    c.GetOpvangtehuisnaam());

                clientlistvm.AddClient(clientvm);
            }

            var oeclvm = new GebruikerViewModel.OpvoederEnClientListViewModel(opvoederlistvm, clientlistvm);

            return View(oeclvm);
        }

        public ActionResult CreateOpvoeder()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var covm = new GebruikerViewModel.CreateOpvoederViewModel
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };
            return View(covm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOpvoeder(GebruikerViewModel.CreateOpvoederViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!ImageIsValidType(model.ImageUpload))
            {
                ModelState.AddModelError("ImageUpload", "Dit is geen foto");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_gebruikerRepository.FindByUsername(model.GebruikersNaam) != null)
                    {
                        this.AddNotification("Er is al reeds iemand met deze gebruikersnaam", NotificationType.ERROR);
                        return RedirectToAction("CreateOpvoeder");
                    }



                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrytwachtwoord = crypto.Compute(model.Wachtwoord);

                    var opvoeder = new Opvoeder(model.Naam, model.Voornaam,
                        _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam,
                        model.Email, encrytwachtwoord, crypto.Salt, model.GeboorteDatum, ImageUploadProfielAfbeelding(model.ImageUpload));

                    _gebruikerRepository.AddOpvoeder(opvoeder);
                    _gebruikerRepository.SaveChanges();

                    this.AddNotification("Opvoeder toegevoegd", NotificationType.SUCCESS);
                    return RedirectToAction("AdminIndex");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            var covm =
                new GebruikerViewModel.CreateOpvoederViewModel(
                    _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList());

            return View(covm);
        }

        public ActionResult CreateClient()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var ccvm = new GebruikerViewModel.CreateClientViewModel();

            if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
            {
                ccvm.SetOpvangtehuizen(_opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList());
            }
            else
            {
                ccvm.AddOpvangtehuis(_gebruikerRepository.FindById((int)Session["gebruiker"]).GetOpvangtehuisnaam());
            }

            return View(ccvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient(GebruikerViewModel.CreateClientViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!ImageIsValidType(model.ImageUpload))
            {
                ModelState.AddModelError("ImageUpload", "Dit is geen foto");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.GeselecteerdOpvangtehuisId != null)
                    {

                        if (_gebruikerRepository.FindByUsername(model.GebruikersNaam) != null)
                        {
                            this.AddNotification("Er is al reeds iemand met deze gebruikersnaam", NotificationType.ERROR);
                            return RedirectToAction("CreateClient");
                        }

                        var crypto = new SimpleCrypto.PBKDF2();
                        var encrytwachtwoord = crypto.Compute(model.Wachtwoord);

                        var client = new Client(model.Naam, model.Voornaam,
                            _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam,
                            model.Email, encrytwachtwoord, crypto.Salt, model.GeboorteDatum, ImageUploadProfielAfbeelding(model.ImageUpload));

                        _gebruikerRepository.AddClient(client);
                        _gebruikerRepository.SaveChanges();

                        if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
                        {
                            this.AddNotification("Cliënt toegevoegd", NotificationType.SUCCESS);
                            return RedirectToAction("AdminIndex");
                        }

                        this.AddNotification("Cliënt toegevoegd", NotificationType.SUCCESS);
                        return RedirectToAction("OpvoederIndex");

                    }
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            var ccvm = new GebruikerViewModel.CreateClientViewModel
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };

            return View(ccvm);
        }

        public ActionResult Details(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);
            GebruikerViewModel.DetailViewModel dvm = null;

            if (gebruiker != null)
            {
                var type = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().Name;
                dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                    gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuis(), type, gebruiker.ImageUrl);

                if (gebruiker is Client)
                {
                    var client = (Client)gebruiker;
                    foreach (var s in client.GetSancties())
                    {
                        dvm.AddSanctie(new GebruikerViewModel.SanctieViewModel(s.Rede, s.BeginDatum, s.EindDatum, s.GetstrafNaam()));
                    }
                }
            }

            return View("Details", dvm);
        }


        /*public ActionResult Delete()
        {
            return View();
        }*/

        public ActionResult Delete(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);
            GebruikerViewModel.DetailViewModel dvm = null;

            if (gebruiker != null)
            {
                string type = gebruiker.GetType().ToString();
                dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                    gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuis(), type, gebruiker.ImageUrl);
            }
            return View(dvm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            try
            {
                _gebruikerRepository.DeleteGebruiker(id);
                _gebruikerRepository.SaveChanges();

                this.AddNotification("De gebruiker is verwijderd", NotificationType.SUCCESS);
                return RedirectToAction("AdminIndex");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);
            var type = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().Name;

            var evm = new GebruikerViewModel.EditViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuisnaam(), type);

            if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
            {
                evm.SetOpvangtehuizen(_opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList());
            }
            else
            {
                evm.AddOpvangtehuis(_gebruikerRepository.FindById((int)Session["gebruiker"]).GetOpvangtehuisnaam());
            }

            return View(evm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GebruikerViewModel.EditViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gebruiker = _gebruikerRepository.FindById(model.Id);
                    gebruiker.EditGebruiker(model.Naam, model.Voornaam,
                        _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam,
                        model.Email, model.GeboorteDatum);

                    _gebruikerRepository.UpdateGebruiker(gebruiker);
                    _gebruikerRepository.SaveChanges();

                    this.AddNotification("De gebruiker is aangepast", NotificationType.SUCCESS);

                    if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
                    {
                        return RedirectToAction("AdminIndex");
                    }

                    return RedirectToAction("OpvoederIndex");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }
            return View(model);

        }

        public ActionResult CreateSanctie(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var svm = new GebruikerViewModel.SanctieViewModel(id, _gebruikerRepository.FindById(id).GiveFullName());
            svm.SetStraffen(opvangtehuis.GetStraffen().Select(s => s.Naam).ToList());
            return View(svm);
        }

        [HttpPost]
        public ActionResult CreateSanctie(GebruikerViewModel.SanctieViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gebruiker = (Client)_gebruikerRepository.FindById(model.Id);
                    var opvangtehuis = _gebruikerRepository.FindById(gebruiker.Id).Opvangtehuis;
                    gebruiker.AddSanctie(model.Rede, model.Date, model.AantalDagen, opvangtehuis.FindStrafByName(model.GeselecteerdeStraf));
                    _gebruikerRepository.SaveChanges();

                    this.AddNotification("Een sanctie is toegevoegd", NotificationType.SUCCESS);
                    return RedirectToAction("OpvoederIndex");
                }
                catch (ApplicationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            this.AddNotification("De sanctie kan niet worden gemaakt", NotificationType.ERROR);
            return RedirectToAction("CreateSanctie");

        }

        public ActionResult Sancties()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
            var slvm = new GebruikerViewModel.SanctieListViewModel();

            foreach (var s in client.GetAppliedSancties())
            {
                slvm.AddSanctie(new GebruikerViewModel.SanctieViewModel(s.Rede, s.BeginDatum, s.EindDatum, s.GetstrafNaam(), s.GetStrafImageUrl()));
            }

            return View(slvm);
        }

        public ActionResult KamerControle()
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
            var kamercontrole = client.GetTodaysKamerControle();
            var lkcivm = new GebruikerViewModel.ListKamerControleItemsViewmodel();
            var kclivm = new GebruikerViewModel.KamerControleListIndexViewModel(client.Id);

            if (kamercontrole != null)
            {
                //Kamercontrole items
                foreach (var i in kamercontrole.KamerControleItems)
                {
                    lkcivm.AddKamerControleItem(new GebruikerViewModel.KamerControleItemViewModel(i.GetControleOpdrachtImageUrl(), i.GetControleOpdrachtTitel(), i.GetControleOpdrachtBeschrijving(), i.OpdrachtGedaanControle, i.Uitleg));
                }
            }

            //Overzicht van de kamercontroles
            foreach (var i in client.GetKamerControles())
            {
                kclivm.AddKamerControleIndexItem(new GebruikerViewModel.KamerControleIndexViewModel(i.Id, i.Datum, i.IsAllesInOrde()));
            }

            var kccvm = new GebruikerViewModel.KamerControleClientViewModel(lkcivm, kclivm);


            return View(kccvm);
        }


        public ActionResult KamerControleIndex(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var client = (Client)_gebruikerRepository.FindById(id);
            Session["client"] = id;
            var kclivm = new GebruikerViewModel.KamerControleListIndexViewModel(client.Id);

            foreach (var i in client.GetKamerControles())
            {
                kclivm.AddKamerControleIndexItem(new GebruikerViewModel.KamerControleIndexViewModel(i.Id, i.Datum, i.IsAllesInOrde()));
            }

            return View(kclivm);
        }

        public ActionResult KamerControleOpvoeder(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var client = (Client)_gebruikerRepository.FindById(id);
            var opvangtehuis = _gebruikerRepository.FindById(client.Id).Opvangtehuis;
            var lkcivm = new GebruikerViewModel.ListKamerControleItemsViewmodel();
            var kamercontrole = client.ViewKamerControle(opvangtehuis.GetKamerControleOpdrachten());

            foreach (var i in kamercontrole.KamerControleItems)
            {
                lkcivm.AddKamerControleItem(new GebruikerViewModel.KamerControleItemViewModel(i.GetControleOpdrachtTitel(), i.GetControleOpdrachtBeschrijving(), i.OpdrachtGedaanControle, i.Uitleg));
            }

            return View(lkcivm);
        }

        [HttpPost]
        public ActionResult KamerControleOpvoeder(GebruikerViewModel.ListKamerControleItemsViewmodel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["client"]);
            var opvangtehuis = _gebruikerRepository.FindById(client.Id).Opvangtehuis;
            var kamercontrole = client.ViewKamerControle(opvangtehuis.GetKamerControleOpdrachten());

            foreach (var i in kamercontrole.KamerControleItems)
            {
                foreach (var ivm in model.KamerControleItems.Where(m => m.Titel == i.GetControleOpdrachtTitel()))
                {
                    i.OpdrachtGedaanControle = ivm.DoneOpvoeder;
                    i.Uitleg = ivm.Uitleg;
                }
            }
            _gebruikerRepository.SaveChanges();

            return RedirectToAction("KamerControleOpvoeder");
        }


        public ActionResult Forum(int id)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            Client client;
            Opvoeder opvoeder;
            string type;

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);

            if (gebruiker is Client)
            {
                client = (Client)gebruiker;
                opvoeder = (Opvoeder)_gebruikerRepository.FindById(id);
                type = "client";
            }
            else
            {
                opvoeder = (Opvoeder)gebruiker;
                client = (Client)_gebruikerRepository.FindById(id);
                type = "Opvoeder";

            }

            var forum = client.GetForum(opvoeder, client);
            var fvm = new GebruikerViewModel.ForumViewModel(forum.Id, type);

            foreach (var p in forum.Posts)
            {
                var mine = p.Gebruiker == gebruiker;
                fvm.AddPost(new GebruikerViewModel.PostViewModel(p.Gebruiker.GiveFullName(), p.TimeStamp, p.Boodschap, mine));
            }

            return View(fvm);
        }

        [HttpPost]
        public ActionResult Forum(GebruikerViewModel.ForumViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            if (ModelState.IsValid)
            {
                var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);

                if (gebruiker is Client)
                {
                    var client = (Client)gebruiker;
                    client.GetForumById(model.ForumId).AddPost(new Post(model.Post, client));
                }
                else
                {
                    var opvoeder = (Opvoeder)gebruiker;
                    opvoeder.GetForumById(model.ForumId).AddPost(new Post(model.Post, opvoeder));
                }

                _gebruikerRepository.SaveChanges();

                this.AddNotification("Uw boodschap is gepost", NotificationType.SUCCESS);
                return RedirectToAction("Forum");
            }
            return View();
        }

        #region helper
        public string ImageUploadProfielAfbeelding(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/ProfielAfbeelding"), pic);

                file.SaveAs(path);

                return path;
            }
            return "~/Content/Images/Aanduidingen/vraagteken.png";
        }

        public bool ImageIsValidType(HttpPostedFileBase file)
        {
            var validImageTypes = new[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (validImageTypes.Contains(file.ContentType))
            {
                return true;
            }
            return false;
        }

        public ActionResult UserStillLoggedIn()
        {
            if (Session["gebruiker"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            return null;
        }
        #endregion


    }
}