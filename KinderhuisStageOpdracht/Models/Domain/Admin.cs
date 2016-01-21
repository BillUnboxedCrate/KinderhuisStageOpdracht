using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Admin : Gebruiker
    {
        public List<Client> Clients { get; set; }
        public List<Opvoeder> Opvoeders { get; set; } 
    }
}