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

        public DateTime Datum { get; set; }

        public KamerToDo()
        {
            KamerToDoItems = new List<KamerToDoItem>();
        }

        public void AddKamerToDoItem(KamerToDoItem item)
        {
            KamerToDoItems.Add(item);
        }
    }
}