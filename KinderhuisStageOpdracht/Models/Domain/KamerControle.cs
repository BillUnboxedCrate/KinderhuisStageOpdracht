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
            AllesInOrde = IsAllesInOrde();
        }

        public void AddKamerControleItem(KamerControleItem item)
        {
            KamerControleItems.Add(item);
        }

       
        public bool IsAllesInOrde()
        {
            var counter = 0;
            foreach (var i in KamerControleItems)
            {
                if (i.OpdrachtGedaanControle)
                {
                    counter++;
                }
            }
            return KamerControleItems.Count == counter;
        }
       

    }
}