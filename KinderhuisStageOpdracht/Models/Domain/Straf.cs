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
        public bool StrafOfBeloning { get; set; }
        public string ImageUrl { get; set; }

        public Straf() { }

        public Straf(string naam, bool strafofbeloning)
        {
            Naam = naam;
            StrafOfBeloning = strafofbeloning;
            ImageUrl = BeloningOfStrafImage(strafofbeloning);
        }

        private string BeloningOfStrafImage(bool strafofbeloning)
        {
            return strafofbeloning ? "/Content/Images/StrafImages/Straf.png" : "/Content/Images/StrafImages/Beloning.png";
        }
    }
}