using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvangtehuis
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public string Straat { get; set; }
        public string StraatNummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode { get; set; }

        public virtual ICollection<Suggestie> Suggesties { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }

        //public virtual ICollection<Gebruiker> Gebruikers { get; set; }

        public Opvangtehuis()
        {
            Suggesties = new List<Suggestie>();
            Menus = new List<Menu>();
            //Gebruikers = new List<Gebruiker>();
        }
        
        public void AddSuggestie(Suggestie suggestie)
        {
            Suggesties.Add(suggestie);
        }

        public void AddMenu(Menu menu)
        {
            
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1} {2}\n{3} {4}", Naam, Straat, StraatNummer, Postcode, Gemeente);
        }

        /*
        public List<Gebruiker> GetGebruikers()
        {
            return Gebruikers.OrderBy(g => g.Voornaam).ToList();
        }
        */
    }
}