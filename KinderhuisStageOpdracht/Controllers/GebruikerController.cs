using System;
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
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            if (Session["gebruiker"] == null || !Request.IsAuthenticated)
            {
                return View("Error");
            }

            return View();
        }

        public ActionResult OpvoederOverzicht()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            var id = (int)Session["gebruiker"];
            var client = (Client)_gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());

            var clientlistvm = new GebruikerViewModel.OpvoederListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().Where(c => c.Opvangtehuis.Id == client.Opvangtehuis.Id).ToList();

            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder)gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel(o.Id, o.Voornaam, o.GiveFullName(), o.ImageUrl, false);
                clientlistvm.AddOpvoeder(opvoedervm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult OpvoederIndex()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
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
                var clientvm = new GebruikerViewModel.ClientViewModel(c.Id, c.GiveFullName(), c.Voornaam, c.ImageUrl, false);

                clientlistvm.AddClient(clientvm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult AdminIndex(string searchString)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
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
            var leefgroeplistvm = new GebruikerViewModel.LeefgroepListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().ToList();
            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().ToList();

            List<Opvangtehuis> opvangtehuizen = _opvangtehuisRepository.FindAll().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                opvoeders = opvoeders.Where(s => s.Naam.ToLower().Contains(searchString.ToLower()) || s.Voornaam.ToLower().Contains(searchString.ToLower())).ToList();
                clients = clients.Where(s => s.Naam.ToLower().Contains(searchString.ToLower()) || s.Voornaam.ToLower().Contains(searchString.ToLower())).ToList();
            }


            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder)gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel(o.Id, o.GiveFullName(),
                    o.GetOpvangtehuisnaam(), o.IsStagair);

                opvoederlistvm.AddOpvoeder(opvoedervm);
            }

            foreach (var gebruiker in clients)
            {
                var c = (Client)gebruiker;
                var clientvm = new GebruikerViewModel.ClientViewModel(c.Id, c.GiveFullName(),
                    c.GetOpvangtehuisnaam());

                clientlistvm.AddClient(clientvm);
            }

            foreach (var opvangtehuis in opvangtehuizen)
            {
                leefgroeplistvm.AddLeefgroep(new GebruikerViewModel.LeefgroepViewModel(opvangtehuis.Id, opvangtehuis.Naam,
                    opvangtehuis.ToString()));
            }



            var oeclvm = new GebruikerViewModel.OpvoederEnClientListViewModel(opvoederlistvm, clientlistvm, leefgroeplistvm);

            return View(oeclvm);
        }

        public ActionResult CreateOpvoeder()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
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
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
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
                        _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam, encrytwachtwoord, crypto.Salt, ImageUploadProfielAfbeelding(model.ImageUpload), model.IsStagair);

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
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Client)
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var ccvm = new GebruikerViewModel.CreateClientViewModel((_gebruikerRepository.FindById((int)Session["gebruiker"])).GetType().Name);

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
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Client)
            {
                return ReturnToLogin();
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
                             encrytwachtwoord, crypto.Salt);

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

            var ccvm = new GebruikerViewModel.CreateClientViewModel()
            {
                GebruikerType = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().Name,
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };

            return View(ccvm);
        }

        public ActionResult CreateLeefgroep()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateLeefgroep(GebruikerViewModel.LeefgroepViewModel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
            }
            if (ModelState.IsValid)
            {
                var opvangtehuis = new Opvangtehuis(model.Naam, model.Straat, model.StraatNummer, model.Gemeente, model.Postcode);
                _opvangtehuisRepository.AddLeefgroep(opvangtehuis);
                _opvangtehuisRepository.SaveChanges();

                this.AddNotification("De leefgroep is toegevoegd", NotificationType.SUCCESS);
                return RedirectToAction("AdminIndex");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Client)
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            try
            {
                var gebruiker = _gebruikerRepository.FindById(id);

                if (gebruiker != null)
                {
                    var type = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().Name;
                    var dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                        gebruiker.Gebruikersnaam, gebruiker.GetOpvangtehuis(), type, gebruiker.ImageUrl);

                    if (gebruiker is Opvoeder)
                    {
                        Opvoeder opvoeder = (Opvoeder)gebruiker;
                        dvm.IsStagair = opvoeder.IsStagair;
                    }

                    if (gebruiker is Client)
                    {
                        var client = (Client)gebruiker;
                        foreach (var s in client.GetAppliedSancties())
                        {
                            dvm.AddSanctie(new GebruikerViewModel.SanctieViewModel(s.Rede, s.BeginDatum, s.EindDatum,
                                s.GetstrafNaam()));
                        }

                        foreach (var track in client.GetTimeTrackList())
                        {
                            dvm.AddTimeTrack(new GebruikerViewModel.TimeTrackerViewModel(track.Aanmelden));
                        }
                    }
                    return View("Details", dvm);
                }

                throw new NullReferenceException();
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
                {
                    return RedirectToAction("AdminIndex");
                }
                if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder)
                {
                    return RedirectToAction("OpvoederIndex");
                }

            }

            return View();
        }


        /*public ActionResult Delete()
        {
            return View();
        }*/

        public ActionResult Delete(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            try
            {
                var gebruiker = _gebruikerRepository.FindById(id);

                if (gebruiker != null)
                {
                    string type = gebruiker.GetType().ToString();
                    var dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                        gebruiker.Gebruikersnaam, gebruiker.GetOpvangtehuis(), type, gebruiker.ImageUrl);

                    return View(dvm);
                }
                throw new NullReferenceException();
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("AdminIndex");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin))
            {
                return ReturnToLogin();
            }

            try
            {
                var gebruiker = _gebruikerRepository.FindById(id);
                gebruiker.DeleteGebruiker();
                _gebruikerRepository.SaveChanges();

                this.AddNotification("De gebruiker is verwijderd", NotificationType.SUCCESS);
                return RedirectToAction("AdminIndex");
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Client)
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            try
            {
                var gebruiker = _gebruikerRepository.FindById(id);
                var type = _gebruikerRepository.FindById((int)Session["gebruiker"]).GetType().Name;

                var evm = new GebruikerViewModel.EditViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                     gebruiker.Gebruikersnaam, gebruiker.GetOpvangtehuisnaam(), type, gebruiker.ImageUrl);

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
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder)
                {
                    return RedirectToAction("OpvoederIndex");
                }
                if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
                {
                    return RedirectToAction("AdminIndex");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GebruikerViewModel.EditViewModel model)
        {
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Client)
            {
                return ReturnToLogin();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gebruiker = _gebruikerRepository.FindById(model.Id);
                    gebruiker.EditGebruiker(model.Naam, model.Voornaam,
                        _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam,
                     ImageUploadProfielAfbeelding(model.Image));

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

        public ActionResult GestrafteOverzicht()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().Where(c => c.Opvangtehuis.Id == opvangtehuis.Id).ToList();
            var slvm = new GebruikerViewModel.SanctieListViewModel();

            foreach (var client in clients)
            {
                var c = (Client)client;
                foreach (var s in c.GetAppliedSancties())
                {
                    slvm.AddSanctie(new GebruikerViewModel.SanctieViewModel(c.GiveFullName(), s.Rede, s.BeginDatum, s.EindDatum, s.GetstrafNaam()));
                }

            }
            return View(slvm);
        }

        public ActionResult CreateSanctie(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            try
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                var svm = new GebruikerViewModel.SanctieViewModel(id, _gebruikerRepository.FindById(id).GiveFullName());
                svm.SetStraffen(opvangtehuis.GetStraffen().Select(s => s.Naam).ToList());
                return View(svm);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex");
            }
        }

        [HttpPost]
        public ActionResult CreateSanctie(GebruikerViewModel.SanctieViewModel model)
        {
            if (UserStillLoggedIn() && !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }
            Opvangtehuis opvangtehuis;
            if (ModelState.IsValid)
            {
                try
                {
                    var gebruiker = (Client)_gebruikerRepository.FindById(model.Id);
                    opvangtehuis = _gebruikerRepository.FindById(gebruiker.Id).Opvangtehuis;
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

            opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var svm = new GebruikerViewModel.SanctieViewModel(model.Id, _gebruikerRepository.FindById(model.Id).GiveFullName());
            svm.SetStraffen(opvangtehuis.GetStraffen().Select(s => s.Naam).ToList());
            return View(svm);

        }

        public ActionResult EditSanctie(int id)
        {
            if (UserStillLoggedIn() && !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            return View("Sanctie");
        }

        public ActionResult Sancties()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
            var slvm = new GebruikerViewModel.SanctieListViewModel();

            foreach (var s in client.GetAppliedSancties())
            {
                slvm.AddSanctie(new GebruikerViewModel.SanctieViewModel(s.Rede, s.BeginDatum, s.EindDatum, s.GetstrafNaam(), s.GetStrafImageUrl(), s.GetIfStrafOrBeloning()));
            }

            return View(slvm);
        }

        public ActionResult KamerControle()
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Client))
            {
                return ReturnToLogin();
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
                    lkcivm.AddKamerControleItem(new GebruikerViewModel.KamerControleItemViewModel(i.GetControleOpdrachtImageUrl(), i.GetControleOpdrachtTitel(), i.OpdrachtGedaanControle, i.Uitleg));
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

        public ActionResult KamerControleOpvoeder(int id)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            try
            {
                var client = (Client)_gebruikerRepository.FindById(id);
                //Session["client"] = id;
                var opvangtehuis = _gebruikerRepository.FindById(client.Id).Opvangtehuis;
                var lkcivm = new GebruikerViewModel.ListKamerControleItemsViewmodel();
                var kclivm = new GebruikerViewModel.KamerControleListIndexViewModel(client.Id);
                var kamercontrole = client.ViewKamerControle(opvangtehuis.GetKamerControleOpdrachten());

                foreach (var i in kamercontrole.KamerControleItems)
                {
                    lkcivm.AddKamerControleItem(new GebruikerViewModel.KamerControleItemViewModel(i.GetControleOpdrachtTitel(), i.OpdrachtGedaanControle, i.Uitleg));
                }

                foreach (var i in client.GetKamerControles())
                {
                    kclivm.AddKamerControleIndexItem(new GebruikerViewModel.KamerControleIndexViewModel(i.Id, i.Datum, i.IsAllesInOrde()));
                }

                lkcivm.KamerControleListIndexViewModel = kclivm;

                _gebruikerRepository.SaveChanges();


                return View(lkcivm);
            }
            catch (NullReferenceException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("OpvoederIndex");
            }
        }

        [HttpPost]
        public ActionResult KamerControleOpvoeder(GebruikerViewModel.ListKamerControleItemsViewmodel model)
        {
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var client = (Client)_gebruikerRepository.FindById(model.KamerControleListIndexViewModel.ClientId);
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
            this.AddNotification("De wijzigingen zijn opgeslagen", NotificationType.SUCCESS);
            return RedirectToAction("KamerControleOpvoeder");
        }


        public ActionResult Forum(int id)
        {
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
            {
                return ReturnToLogin();
            }

            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);

            try
            {
                Client client;
                Opvoeder opvoeder;
                string type;

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
                if (forum == null)
                    _gebruikerRepository.SaveChanges();
                var fvm = new GebruikerViewModel.ForumViewModel(forum.Id, type);

                foreach (var p in forum.Posts)
                {
                    var mine = p.Gebruiker == gebruiker;
                    fvm.AddPost(new GebruikerViewModel.PostViewModel(p.Gebruiker.GiveFullName(), p.TimeStamp, p.Boodschap, mine, p.Gebruiker.ImageUrl));
                }

                return View(fvm);
            }
            catch (NullReferenceException e)
            {
                if (gebruiker is Client)
                {
                    return RedirectToAction("ClientIndex");
                }
                if (gebruiker is Opvoeder)
                {
                    return RedirectToAction("OpvoederIndex");
                }

            }
            return View();

        }

        [HttpPost]
        public ActionResult Forum(GebruikerViewModel.ForumViewModel model)
        {
            if (UserStillLoggedIn() || _gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
            {
                return ReturnToLogin();
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

                this.AddNotification("Je boodschap is gepost", NotificationType.SUCCESS);
                return RedirectToAction("Forum");
            }
            return View();
        }

        public ActionResult Instellingen()
        {
            if (UserStillLoggedIn())
            {
                return ReturnToLogin();
            }

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);
            var ivm = new GebruikerViewModel.InstellingenViewModel(gebruiker.GetType().Name, gebruiker.BackgroundUrl, gebruiker.ImageUrl);

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Instellingen(GebruikerViewModel.InstellingenViewModel model)
        {
            if (UserStillLoggedIn())
            {
                return ReturnToLogin();
            }

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);
            if (ModelState.IsValid)
            {
                if (model.AvatarUpload != null)
                {
                    gebruiker.AddImage(ImageUploadProfielAfbeelding(model.AvatarUpload));
                    Session["profileimageurl"] = gebruiker.ImageUrl;
                }

                if (model.BackgroundUpload != null)
                {
                    gebruiker.AddBackground(ImageUploadBackgroundAfbeelding(model.BackgroundUpload));
                    Session["backgroundurl"] = gebruiker.BackgroundUrl;
                }
                _gebruikerRepository.SaveChanges();
                this.AddNotification("De veranderingen zijn opgeslagen.", NotificationType.SUCCESS);

            }

            var ivm = new GebruikerViewModel.InstellingenViewModel(gebruiker.GetType().Name, gebruiker.BackgroundUrl, gebruiker.ImageUrl);
            return View(ivm);
        }

        public ActionResult WachtwoordAanpassen()
        {
            if (UserStillLoggedIn())
            {
                return ReturnToLogin();
            }

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);
            var wcvm = new GebruikerViewModel.WachtwoordChangeViewModel(gebruiker.GetType().Name);
            return View(wcvm);
        }

        [HttpPost]
        public ActionResult WachtwoordAanpassen(GebruikerViewModel.WachtwoordChangeViewModel model)
        {
            if (UserStillLoggedIn())
            {
                return ReturnToLogin();
            }

            var gebruiker = _gebruikerRepository.FindById((int)Session["gebruiker"]);
            if (ModelState.IsValid)
            {
                if (IsValid(model.Wachtwoord, gebruiker.Gebruikersnaam))
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrytwachtwoord = crypto.Compute(model.NieuwWachtwoord);

                    gebruiker.WachtwoordAanpassen(encrytwachtwoord, crypto.Salt);
                    _gebruikerRepository.SaveChanges();

                    this.AddNotification("Je wachtwoord is aangepast", NotificationType.SUCCESS);
                    return RedirectToAction("Instellingen");
                }
                ModelState.AddModelError("Wachtwoord", "Het wachtwoord dat is ingegeven is niet correct");
            }
            var wcvm = new GebruikerViewModel.WachtwoordChangeViewModel(gebruiker.GetType().Name);
            return View(wcvm);
        }


        #region helper
        public string ImageUploadProfielAfbeelding(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("/Content/Images/ProfielAfbeelding"), pic);

                file.SaveAs(path);

                return "/Content/Images/ProfielAfbeelding/" + pic;
            }
            return "/Content/Images/ProfielAfbeelding/default.png";
        }

        public string ImageUploadBackgroundAfbeelding(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("/Content/Images/Backgrounds"), pic);

                file.SaveAs(path);

                return "/Content/Images/Backgrounds/" + pic;
            }
            return "/Content/Images/Aanduidingen/vraagteken.png";
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


        public bool IsValid(string password, string username)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            var gebruiker = _gebruikerRepository.FindByUsername(username);
            if (gebruiker != null)
            {
                if (gebruiker.Wachtwoord == crypto.Compute(password, gebruiker.Salt))
                {
                    isValid = true;
                }
            }

            return isValid;
        }
        #endregion


    }
}