using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Klacht
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime TimeStamp { get; set; }
        public Client Client { get; set; }

        public Klacht()
        {
            
        }
    }
}