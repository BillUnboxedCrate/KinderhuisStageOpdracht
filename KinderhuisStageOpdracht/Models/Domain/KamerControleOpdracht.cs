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
        public string ImageUrl { get; set; }

        public KamerControleOpdracht() { }

        public KamerControleOpdracht(string titel, string imageUrl)
        {
            Titel = titel;
            ImageUrl = imageUrl;
        }
    }
}