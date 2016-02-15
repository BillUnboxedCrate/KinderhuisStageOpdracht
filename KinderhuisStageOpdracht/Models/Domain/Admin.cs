using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Admin : Gebruiker
    {
        public ICollection<Client> Clients { get; set; }
        public ICollection<Opvoeder> Opvoeders { get; set; } 
    }
}