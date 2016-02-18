using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class GebruikerViewModel
    {
        public class OpvoederEnClientListViewModel
        {
            public OpvoederListViewModel Olmv { get; set; }
            public ClientListViewModel Clvm { get; set; }
        }

        public class OpvoederViewModel
        {
            public int Id { get; set; }
            public string Voornaam { get; set; }
            public string Naam { get; set; }
            public string Email { get; set; }
        }

        public class OpvoederListViewModel
        {
            public List<OpvoederViewModel> Opvoeders {get; set; }

            public OpvoederListViewModel()
            {
                Opvoeders = new List<OpvoederViewModel>();
            }
        }

        public class ClientViewModel
        {
            public int Id { get; set; }
            public string Voornaam { get; set; }
            public string Naam { get; set; }
        }

        public class ClientListViewModel
        {
            public List<ClientViewModel> Clients { get; set; }

            public ClientListViewModel()
            {
                Clients = new List<ClientViewModel>();
            }
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