using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerControle
    {
        public int Id { get; set; }
        //public virtual ICollection<KamerControleOpdracht> KamerControleItems { get; set; }
        public bool AllesInOrde { get; set; }

        public DateTime Datum { get; set; }

        public KamerControle()
        {
            //KamerControleItems = new List<KamerControleOpdracht>();
        }

        public KamerControle(DateTime datum)
        {
            Datum = datum;
            //KamerControleItems = new List<KamerControleOpdracht>();
        }

        public void AddKamerToDoItem(KamerControleOpdracht item)
        {
            //KamerControleItems.Add(item);
        }

       

    }
}