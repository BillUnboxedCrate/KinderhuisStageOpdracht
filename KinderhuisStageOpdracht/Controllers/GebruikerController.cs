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

        public GebruikerController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Gebruiker
        public ActionResult ClientIndex(int id)
        {
            var client = _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(client.GetType());
            return View();
        }

        public ActionResult OpvoederIndex(int id)
        {
            var opvoeder = _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(opvoeder.GetType());
            return View();
        }

        public ActionResult AdminIndex(int id)
        {
            var admin = _gebruikerRepository.FindById(id);
            System.Diagnostics.Debug.WriteLine(admin.GetType());
            return View();
        }
    }
}