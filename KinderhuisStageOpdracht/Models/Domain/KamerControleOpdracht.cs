using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerControleOpdracht
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public string ImageUrl { get; set; }

        public KamerControleOpdracht() { }

        public KamerControleOpdracht(string titel, string beschrijving, string imageUrl)
        {
            Titel = titel;
            Beschrijving = beschrijving;
            ImageUrl = imageUrl;
        }
    }
}