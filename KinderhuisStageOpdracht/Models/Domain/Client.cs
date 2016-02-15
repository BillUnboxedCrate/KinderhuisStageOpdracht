using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Client : Gebruiker
    {
        public virtual Planning Planning { get; set; }
        public virtual KamerToDo KamerToDo { get; set; }
    }
}