using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerToDo
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        //public Opvoeder Opvoeder { get; set; }
        public virtual ICollection<KamerToDoItem> KamerToDoItems { get; set; } 
    }
}