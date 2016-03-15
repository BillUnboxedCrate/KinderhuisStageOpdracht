using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Planning
    {
        public int Id { get; set; }
        //public virtual Client Client { get; set; }
        public virtual ICollection<PlanningItem> PlanningItems { get; set; }
        public DateTime BegindagWeek { get; set; }
        public DateTime EinddagWeek { get; set; }

        public Planning()
        {
            PlanningItems = new List<PlanningItem>();
        }

        public Planning(Client client)
        {
            //Client = client;
            PlanningItems = new List<PlanningItem>();
        }

        public void AddPlanningsItem(PlanningItem item)
        {
            PlanningItems.Add(item);
        }

        public void AddPlanningsItem(string titel, string omschrijving, bool verwijderbaar, DateTime dateTime)
        {
            var item = new PlanningItem(titel, omschrijving,verwijderbaar,dateTime);
            PlanningItems.Add(item);
        }
    }
}