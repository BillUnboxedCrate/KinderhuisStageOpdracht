using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Planning
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public ICollection<PlanningItem> PlanningItems { get; set; } 
    }
}