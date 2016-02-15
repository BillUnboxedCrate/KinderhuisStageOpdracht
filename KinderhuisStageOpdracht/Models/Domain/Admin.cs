using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Admin : Gebruiker
    {
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Opvoeder> Opvoeders { get; set; }

        public Admin()
        {
            Clients = new List<Client>();
            Opvoeders = new List<Opvoeder>();
        }
    }
}