using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            var client = (Client) _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());
            return View();
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
            return View();
        }

        //[Authorize]
        public ActionResult AdminIndex()
        {
            if (Session["gebruiker"] == null)
            {
                return View("Error");
            }
            var id = (int) Session["gebruiker"];
            //Session["gebruiker"] = id;
            var admin = (Admin) _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(admin.GetType());

            var opvoederlistvm = new GebruikerViewModel.OpvoederListViewModel();
            var clientlistvm = new GebruikerViewModel.ClientListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().ToList();
            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().ToList();

            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder) gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel
                {
                    Id = o.Id,
                    Naam = o.Naam,
                    Voornaam = o.Voornaam,
                    Email = o.Email
                };
                opvoederlistvm.Opvoeders.Add(opvoedervm);
            }

            foreach (var gebruiker in clients)
            {
                var c = (Client) gebruiker;
                var clientvm = new GebruikerViewModel.ClientViewModel
                {
                    Id = c.Id,
                    Naam = c.Naam,
                    Voornaam = c.Voornaam,
                    Email = c.Email
                };
                clientlistvm.Clients.Add(clientvm);
            }

            var oeclvm = new GebruikerViewModel.OpvoederEnClientListViewModel()
            {
                Clvm = clientlistvm,
                Olmv = opvoederlistvm
            };

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
                
                var opvoeder = new Opvoeder()
                {
                    Naam = model.Naam,
                    Voornaam = model.Voornaam,
                    Opvangtehuis = _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId),
                    Gebruikersnaam = model.GebruikersNaam,
                    GeboorteDatum = model.GeboorteDatum,
                    Email = model.Email,
                    Wachtwoord = encrytwachtwoord,
                    Salt = crypto.Salt,
                };

                _gebruikerRepository.AddOpvoeder(opvoeder);
                _gebruikerRepository.SaveChanges();

                return RedirectToAction("AdminIndex");
            }

            var covm = new GebruikerViewModel.CreateOpvoederViewModel
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };

            return View(covm);
        }

        public ActionResult CreateClient()
        {
            var ccvm = new GebruikerViewModel.CreateClientViewModel()
            {
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList()
            };
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
                
                var client = new Client()
                {
                    Naam = model.Naam,
                    Voornaam = model.Voornaam,
                    Opvangtehuis = _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId),
                    Gebruikersnaam = model.GebruikersNaam,
                    GeboorteDatum = model.GeboorteDatum,
                    Email = model.Email,
                    Wachtwoord = encrytwachtwoord,
                    Salt = crypto.Salt,
                };

                _gebruikerRepository.AddClient(client);
                _gebruikerRepository.SaveChanges();

                return RedirectToAction("AdminIndex");
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
                dvm = new GebruikerViewModel.DetailViewModel
                {
                    Id = gebruiker.Id,
                    Naam = gebruiker.Naam,
                    Voornaam = gebruiker.Voornaam,
                    Email = gebruiker.Email,
                    GeboorteDatum = gebruiker.GeboorteDatum,
                    GebruikersNaam = gebruiker.Gebruikersnaam,
                    Opvangtehuis = gebruiker.Opvangtehuis.ToString()
                };
            }

            return View("Details", dvm);
        }

        
        /*public ActionResult Delete()
        {
            return View();
        }*/

        [HttpPost]
        public ActionResult Delete(int id)
        {   
            if (_gebruikerRepository.FindById(id) != null)
            {
                _gebruikerRepository.DeleteGebruiker(id);
            }
          

            return View("AdminIndex");
        }

        public ActionResult Edit(int id)
        {
            var gebruiker = _gebruikerRepository.FindById(id);


            var evm = new GebruikerViewModel.EditViewModel
            {
                Id = gebruiker.Id,
                Voornaam = gebruiker.Voornaam,
                Email = gebruiker.Email,
                Naam = gebruiker.Naam,
                GeboorteDatum = gebruiker.GeboorteDatum,
                GebruikersNaam = gebruiker.Gebruikersnaam,
                Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(ho => ho.Naam).ToList(),
                GeselecteerdOpvangtehuisId = gebruiker.Opvangtehuis.Naam
            };

            return View(evm);
        }

        [HttpPost]
        public ActionResult Edit(GebruikerViewModel.EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gebruiker = _gebruikerRepository.FindById(model.Id);
                gebruiker.Naam = model.Naam;
                gebruiker.Voornaam = model.Voornaam;
                gebruiker.Email = model.Email;
                gebruiker.Gebruikersnaam = model.GebruikersNaam;
                gebruiker.Opvangtehuis = _opvangtehuisRepository.FindByName(model.GeselecteerdOpvangtehuisId);
                gebruiker.GeboorteDatum = model.GeboorteDatum;

                _gebruikerRepository.UpdateGebruiker(gebruiker);
                _gebruikerRepository.SaveChanges();

                return RedirectToAction("AdminIndex");
            }
            return View(model);

        }
    }
}