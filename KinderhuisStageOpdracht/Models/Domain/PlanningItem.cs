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
        public DateTime Datum { get; set; }
        public string Actie { get; set; }
        public bool Verwijderbaar { get; set; }

        public PlanningItem() { }

        public PlanningItem(string actie, DateTime datum, bool verwijderbaar)
        {
            Actie = actie;
            Datum = datum;
            Verwijderbaar = verwijderbaar;
        }
    }
}