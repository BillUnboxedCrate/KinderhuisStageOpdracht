using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvoeder : Gebruiker
    {
        public string Email { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public Opvoeder()
        {
            Clients = new List<Client>();
        }
    }
}