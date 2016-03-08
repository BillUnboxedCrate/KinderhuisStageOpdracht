using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Client : Gebruiker
    {
        public virtual ICollection<Planning> Plannings { get; set; }
        public virtual ICollection<KamerToDo> KamerToDos { get; set; }

        public virtual ICollection<Forum> Forums { get; set; }

        public virtual ICollection<Sanctie> Sancties { get; set; } 

        public Client()
        {
            Plannings = new List<Planning>();
            KamerToDos = new List<KamerToDo>();
            Forums = new List<Forum>();
            Sancties = new List<Sanctie>();
        }

        public Client(string naam, string voornaam, Opvangtehuis opvangtehuis, string gebruikersnaam, string email, string wachtwoord, string salt, DateTime geboortedatum)
        {
            Naam = naam;
            Voornaam = voornaam;
            Opvangtehuis = opvangtehuis;
            Gebruikersnaam = gebruikersnaam;
            Email = email;
            Wachtwoord = wachtwoord;
            Salt = salt;
            GeboorteDatum = geboortedatum;
        }

        public void AddKamerToDo(KamerToDo toDo)
        {
            KamerToDos.Add(toDo);
        }

        public void AddForum(Forum forum)
        {
            Forums.Add(forum);
        }

        public void AddSanctie(Sanctie sanctie)
        {
            Sancties.Add(sanctie);
        }

        public void AddSanctie(bool verboden, string genre, string rede, DateTime datum)
        {
            var sanctie = new Sanctie(verboden, genre, rede, datum);

            Sancties.Add(sanctie);
        }
    }
}