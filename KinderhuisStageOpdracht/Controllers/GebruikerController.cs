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

        public GebruikerController(IGebruikerRepository gebruikerRepository, IOpvangtehuisRepository opvangtehuisRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            _opvangtehuisRepository = opvangtehuisRepository;
        }

        // GET: Gebruiker
        //[Authorize]
        public ActionResult ClientIndex()
        {
            if (Session["gebruiker"] == null)
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
            if (Session["gebruiker"] == null)
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
            if (Session["gebruiker"] == null)
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
            var covm = new GebruikerViewModel.CreateOpvoederViewModel
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };
            return View(covm);
        }

        [HttpPost]
        public ActionResult CreateOpvoeder(GebruikerViewModel.CreateOpvoederViewModel model)
        {
            if (ModelState.IsValid)
            {
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

            var covm =
                new GebruikerViewModel.CreateOpvoederViewModel(
                    _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList());

            return View(covm);
        }

        public ActionResult CreateClient()
        {
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
        public ActionResult CreateClient(GebruikerViewModel.CreateClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.GeselecteerdOpvangtehuisId != null)
                {

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

            var ccvm = new GebruikerViewModel.CreateClientViewModel
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };

            return View(ccvm);
        }

        public ActionResult Details(int id)
        {
            var gebruiker = _gebruikerRepository.FindById(id);
            GebruikerViewModel.DetailViewModel dvm = null;

            if (gebruiker != null)
            {
                dvm = new GebruikerViewModel.DetailViewModel(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam,
                    gebruiker.GeboorteDatum, gebruiker.Gebruikersnaam, gebruiker.Email, gebruiker.GetOpvangtehuis());
            }

            return View("Details", dvm);
        }


        /*public ActionResult Delete()
        {
            return View();
        }*/

        public ActionResult Delete(int id)
        {
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
            _gebruikerRepository.DeleteGebruiker(id);
            _gebruikerRepository.SaveChanges();

            this.AddNotification("De gebruiker is verwijderd", NotificationType.SUCCESS);
            return RedirectToAction("AdminIndex");
        }

        public ActionResult Edit(int id)
        {
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
        public ActionResult Edit(GebruikerViewModel.EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gebruiker = _gebruikerRepository.FindById(model.Id);
                gebruiker.EditGebruiker(model.Naam, model.Voornaam, _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId), model.GebruikersNaam, model.Email, model.GeboorteDatum);

                _gebruikerRepository.UpdateGebruiker(gebruiker);
                _gebruikerRepository.SaveChanges();

                this.AddNotification("De gebruiker is aangepast", NotificationType.SUCCESS);

                if (_gebruikerRepository.FindById((int)Session["gebruiker"]) is Admin)
                {
                    return RedirectToAction("AdminIndex");
                }

                return RedirectToAction("OpvoederIndex");

            }
            return View(model);

        }

        public ActionResult ForumIndex()
        {
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
    }
}