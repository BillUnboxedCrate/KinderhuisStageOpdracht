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
            var client = (Client) _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());

            var clientlistvm = new GebruikerViewModel.OpvoederListViewModel();

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().Where(c => c.Opvangtehuis.Id == client.Opvangtehuis.Id).ToList();

            foreach (var gebruiker in opvoeders)
            {
                var o = (Opvoeder)gebruiker;
                var opvoedervm = new GebruikerViewModel.OpvoederViewModel()
                {
                    Id = o.Id,
                    Naam = o.GiveFullName(),
                    Email = o.Email
                };
                clientlistvm.Opvoeders.Add(opvoedervm);
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
                var clientvm = new GebruikerViewModel.ClientViewModel
                {
                    Id = c.Id,
                    Naam = c.GiveFullName(),
                    Email = c.Email
                };
                clientlistvm.Clients.Add(clientvm);
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
                    Naam = o.GiveFullName(),
                    Email = o.Email,
                    Opvangtehuis = o.Opvangtehuis.Naam
                };
                opvoederlistvm.Opvoeders.Add(opvoedervm);
            }

            foreach (var gebruiker in clients)
            {
                var c = (Client) gebruiker;
                var clientvm = new GebruikerViewModel.ClientViewModel
                {
                    Id = c.Id,
                    Naam = c.GiveFullName(),
                    Email = c.Email,
                    Opvangtehuis = c.Opvangtehuis.Naam
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


                this.AddNotification("Opvoeder toegevoegd", NotificationType.SUCCESS);
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
            var ccvm = new GebruikerViewModel.CreateClientViewModel();

            if (_gebruikerRepository.FindById((int) Session["gebruiker"]) is Admin)
            {
                ccvm.Opvangtehuizen = _opvangtehuisRepository.FindAll().Select(oh => oh.Naam).ToList();
            }
            else
            {
                ccvm.Opvangtehuizen.Add(_gebruikerRepository.FindById((int) Session["gebruiker"]).Opvangtehuis.Naam);
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

                    if (_gebruikerRepository.FindById((int) Session["gebruiker"]) is Admin)
                    {
                        this.AddNotification("Cliënt toegevoegd", NotificationType.SUCCESS);
                        return RedirectToAction("AdminIndex");
                    }
                    else
                    {
                        this.AddNotification("Cliënt toegevoegd", NotificationType.SUCCESS);
                        return RedirectToAction("OpvoederIndex");
                    }

                
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

        public ActionResult Delete(int id)
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

                this.AddNotification("De gebruiker is aangepast", NotificationType.SUCCESS);
                return RedirectToAction("AdminIndex");
            }
            return View(model);

        }

        public ActionResult ForumIndex()
        {
            var flvm = new GebruikerViewModel.ForumListViewModel();

            var opvoeder = (Opvoeder)_gebruikerRepository.FindById((int)Session["gebruiker"]);

            foreach (var f in opvoeder.Forums)
            {
                var fvm = new GebruikerViewModel.ForumViewModel
                {
                    Id = f.Id,
                };
                flvm.List.Add(fvm);
            }

            return View(flvm);
        }

        public ActionResult Forum(int id)
        {
            var plvm = new GebruikerViewModel.PostsListViewModel();

            if (_gebruikerRepository.FindById((int) Session["gebruiker"]) is Opvoeder)
            {
                var opvoeder = (Opvoeder)_gebruikerRepository.FindById((int) Session["gebruiker"]);
                var forum = opvoeder.Forums.FirstOrDefault(f => f.Id == id);

                if (forum == null)
                {
                    
                }

                foreach (var p in forum.Posts)
                {
                    var pvm = new GebruikerViewModel.PostViewModel()
                    {
                        Boodschap = p.Boodschap,
                        SendBy = p.Gebruiker.GiveFullName(),
                        TimeStamp = p.TimeStamp
                    };
                    plvm.List.Add(pvm);
                }
                
            }
            else
            {
                var client = (Client)_gebruikerRepository.FindById((int) Session["gebruiker"]);
                var forum = client.Forums.FirstOrDefault(f => f.Id == id);

                foreach (var p in forum.Posts)
                {
                    var pvm = new GebruikerViewModel.PostViewModel()
                    {
                        Boodschap = p.Boodschap,
                        SendBy = p.Gebruiker.GiveFullName(),
                        TimeStamp = p.TimeStamp
                    };
                    plvm.List.Add(pvm);
                }
            }

            return View(plvm);
        }
    }
}