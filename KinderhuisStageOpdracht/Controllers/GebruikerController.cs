using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{
    
    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private int gebruikerId;

        public GebruikerController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Gebruiker
        //[Authorize]
        public ActionResult ClientIndex(int id)
        {
            gebruikerId = id;
            var client = (Client) _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());
            return View();
        }

        //[Authorize]
        public ActionResult OpvoederIndex(int id)
        {
            gebruikerId = id;
            var opvoeder = (Opvoeder)_gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(opvoeder.GetType());
            return View();
        }

        //[Authorize]
        public ActionResult AdminIndex(int id)
        {
            gebruikerId = id;
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
                    Voornaam = c.Voornaam
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateOpvoeder(GebruikerViewModel.CreateOpvoederViewModel model)
        {

            return View();
        }
    }

    
}