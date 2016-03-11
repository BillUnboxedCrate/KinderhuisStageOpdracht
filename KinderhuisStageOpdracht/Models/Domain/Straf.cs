using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Straf
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string ImageUrl { get; set; }

        public Straf() { }

        public Straf(string naam, string imageUrl)
        {
            Naam = naam;
            ImageUrl = imageUrl;
        }
    }
}