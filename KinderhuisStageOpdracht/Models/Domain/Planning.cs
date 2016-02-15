using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Planning
    {
        public int Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<PlanningItem> PlanningItems { get; set; }
        public DateTime BegindagWeek { get; set; }
        public DateTime EinddagWeek { get; set; }

        public Planning()
        {
            PlanningItems = new List<PlanningItem>();
        }
    }
}