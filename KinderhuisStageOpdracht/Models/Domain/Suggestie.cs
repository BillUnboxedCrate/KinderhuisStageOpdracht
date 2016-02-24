using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Suggestie
    {
        public int Id { get; set; }
        public string Beschrijving { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual Client Client { get; set; }
        public string Genre { get; set; }

        
    }
}