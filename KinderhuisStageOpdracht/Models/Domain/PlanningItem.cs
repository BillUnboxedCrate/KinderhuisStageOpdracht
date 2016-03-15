using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class PlanningItem
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public bool Verwijderbaar { get; set; }
        public DateTime DatumTijd { get; set; }

        public PlanningItem() { }

        public PlanningItem(string titel, string omschrijving, bool verwijderbaar, DateTime dateTime)
        {
            Titel = titel;
            Omschrijving = omschrijving;
            Verwijderbaar = verwijderbaar;
            DatumTijd = dateTime;
        }
    }
}