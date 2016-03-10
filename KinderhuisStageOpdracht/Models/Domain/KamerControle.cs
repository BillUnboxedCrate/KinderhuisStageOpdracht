using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerControle
    {
        public int Id { get; set; }
        public virtual ICollection<KamerControleItem> KamerControleItems { get; set; }
        public bool AllesInOrde { get; set; }

        public DateTime Datum { get; set; }

        public KamerControle()
        {
            KamerControleItems = new List<KamerControleItem>();
        }

        public KamerControle(DateTime datum)
        {
            Datum = datum;
            KamerControleItems = new List<KamerControleItem>();
        }

        public void AddKamerControleItem(KamerControleItem item)
        {
            KamerControleItems.Add(item);
        }

       

    }
}