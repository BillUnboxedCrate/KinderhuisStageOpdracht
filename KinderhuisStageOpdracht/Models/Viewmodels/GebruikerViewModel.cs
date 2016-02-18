using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class GebruikerViewModel
    {
        public class ClientViewModel
        {
            public int Id { get; set; }
            public string Voornaam { get; set; }
            public string Naam { get; set; }
        }

        public class RegisterOpvoederViewModel
        {
            public string Naam { get; set; }
            public string Voornaam { get; set; }
            public DateTime GeboorteDatum { get; set; }
            public string GebruikersNaam { get; set; }
            public string Wachtwoord { get; set; }
            public string Email { get; set; }

        }

        public class RegisterClientViewModel
        {
            public string Naam { get; set; }
            public string Voornaam { get; set; }
            public DateTime GeboorteDatum { get; set; }
            public string GebruikersNaam { get; set; }
            public string Wachtwoord { get; set; }
        }
    }
}