using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{

    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IOpvangtehuisRepository _opvangtehuisRepository;
        private readonly IStrafRepository _strafRepository;
        private readonly IKamerControleOpdrachtRepository _kamerControleItemRepository;

        public GebruikerController(IGebruikerRepository gebruikerRepository, IOpvangtehuisRepository opvangtehuisRepository, IStrafRepository strafRepository, IKamerControleOpdrachtRepository kamerControleItemRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            _opvangtehuisRepository = opvangtehuisRepository;
            _strafRepository = strafRepository;
            _kamerControleItemRepository = kamerControleItemRepository;
        }

        // GET: Gebruiker
        //[Authorize]
        public ActionResult ClientIndex()
        {
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
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel(o.Id, o.GiveFullName(), o.Email);
                clientlistvm.AddOpvoeder(opvoedervm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult OpvoederIndex()
        {
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
                var clientvm = new GebruikerViewModel.ClientViewModel(c.Id, c.GiveFullName(), c.Email);

                clientlistvm.AddClient(clientvm);
            }

            return View(clientlistvm);
        }

        //[Authorize]
        public ActionResult AdminIndex()
        {
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
                        model.Email, encrytwachtwoord, crypto.Salt, model.GeboorteDatum);

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
                            model.Email, encrytwachtwoord, crypto.Salt, model.GeboorteDatum);

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
            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);
            GebruikerViewModel.DetailViewModel dvm = null;

            if (gebruiker != null)
            {
                dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                    gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuis());

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
            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);
            GebruikerViewModel.DetailViewModel dvm = null;

            if (gebruiker != null)
            {
                dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                    gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuis());
            }
            return View(dvm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var gebruiker = _gebruikerRepository.FindById(id);


            var evm = new GebruikerViewModel.EditViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuisnaam());

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

        public ActionResult ForumIndex()
        {
            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var flvm = new GebruikerViewModel.ForumListViewModel();

            var opvoeder = (Opvoeder)_gebruikerRepository.FindById((int)Session["gebruiker"]);

            foreach (var f in opvoeder.Forums)
            {
                var fvm = new GebruikerViewModel.ForumViewModel(f.Id);
                flvm.AddForum(fvm);
            }

            return View(flvm);
        }

        public ActionResult Forum(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return View("Error");
            }

            var plvm = new GebruikerViewModel.PostsListViewModel();

            if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder)
            {
                var opvoeder = (Opvoeder)_gebruikerRepository.FindById((int)Session["gebruiker"]);
                var forum = opvoeder.Forums.FirstOrDefault(f => f.Id == id);

                foreach (var p in forum.Posts)
                {
                    var pvm = new GebruikerViewModel.PostViewModel(p.Gebruiker.GiveFullName(), p.TimeStamp, p.Boodschap);
                    plvm.AddPost(pvm);
                }

            }
            else
            {
                var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
                var forum = client.Forums.FirstOrDefault(f => f.Id == id);

                foreach (var p in forum.Posts)
                {
                    var pvm = new GebruikerViewModel.PostViewModel(p.Gebruiker.GiveFullName(), p.TimeStamp, p.Boodschap);
                    plvm.AddPost(pvm);
                }
            }

            return View(plvm);
        }

        public ActionResult CreateSanctie(int id)
        {
            var svm = new GebruikerViewModel.SanctieViewModel(id, _gebruikerRepository.FindById(id).GiveFullName());
            svm.SetStraffen(_strafRepository.FindAll().Select(s => s.Naam).ToList());
            return View(svm);
        }

        [HttpPost]
        public ActionResult CreateSanctie(GebruikerViewModel.SanctieViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gebruiker = (Client)_gebruikerRepository.FindById(model.Id);
                    gebruiker.AddSanctie(model.Rede, model.Date, model.AantalDagen, _strafRepository.FindByNaam(model.GeselecteerdeStraf));
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
            var client = (Client)_gebruikerRepository.FindById((int)Session["gebruiker"]);
            client.ViewKamerControle();

            return View();
        }
    }
}