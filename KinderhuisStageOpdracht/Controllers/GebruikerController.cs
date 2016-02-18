using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Controllers
{
    
    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private Gebruiker gebruiker;

        public GebruikerController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Gebruiker
        //[Authorize]
        public ActionResult ClientIndex(int id)
        {
            //var client = (Client) _gebruikerRepository.FindById(id);
            gebruiker = _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(gebruiker.GetType());
            return View();
        }

        //[Authorize]
        public ActionResult OpvoederIndex(int id)
        {
            //var opvoeder = (Opvoeder)_gebruikerRepository.FindById(id);
            gebruiker = _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(gebruiker.GetType());
            return View();
        }

        //[Authorize]
        public ActionResult AdminIndex(int id)
        {
            //var admin = (Admin) _gebruikerRepository.FindById(id);
            gebruiker = (Admin) _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(gebruiker.GetType());

            List<Gebruiker> opvoeders = _gebruikerRepository.FindAllOpvoeders().ToList();
            List<Gebruiker> clients = _gebruikerRepository.FindAllClients().ToList();            
            
            return View();
        }
    }
}