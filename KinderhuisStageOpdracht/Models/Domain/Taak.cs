using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Taak
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime Datum { get; set; }
        public virtual ICollection<Gebruiker> Gebruikers { get; set; }

        public Taak()
        {
            Gebruikers = new List<Gebruiker>();
        }
    }
}   