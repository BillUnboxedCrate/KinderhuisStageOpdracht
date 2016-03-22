using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvoeder : Gebruiker
    {
        public bool IsStagair { get; set; }

        public virtual ICollection<Forum> Forums { get; set; }

        public Opvoeder()
        {
            Forums = new List<Forum>();
        }

        public Opvoeder(string naam, string voornaam, Opvangtehuis opvangtehuis, string gebruikersnaam, string email, string wachtwoord, string salt, DateTime geboortedatum, string imageUrl, bool isStagair)
        {
            Naam = naam;
            Voornaam = voornaam;
            Opvangtehuis = opvangtehuis;
            Gebruikersnaam = gebruikersnaam;
            Email = email;
            Wachtwoord = wachtwoord;
            Salt = salt;
            GeboorteDatum = geboortedatum;
            ImageUrl = imageUrl;
            IsStagair = isStagair;
        }

        public Forum GetForumById(int id)
        {
            return Forums.FirstOrDefault(f => f.Id == id);
        }   

        public void AddForum(Forum forum)
        {
            Forums.Add(forum);
        }
    }
}