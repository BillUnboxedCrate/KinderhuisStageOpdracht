﻿using System;
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
        
        /*public virtual ICollection<Gebruiker> Gebruikers { get; set; }

        public Opvangtehuis() 
        {
            Gebruikers = new List<Gebruiker>();
        }*/

        public override string ToString()
        {
            return String.Format("{0}\n{1} {2}\n{3} {4}", Naam, Straat, StraatNummer, Postcode, Gemeente);
        }
    }
}