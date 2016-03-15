using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Klacht
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual Client Client { get; set; }

        public Klacht(){ }

        public Klacht(string omschrijving, Client client)
        {
            Omschrijving = omschrijving;
            Client = client;
            TimeStamp = DateTime.Now;
        }
    }
}