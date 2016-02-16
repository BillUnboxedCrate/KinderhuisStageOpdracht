using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class MenuItem
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }
        public string Dag { get; set; }
        public string Voorgerecht { get; set; }
        public string Hoofdgerecht { get; set; }
        public string Dessert { get; set; }
    }
}