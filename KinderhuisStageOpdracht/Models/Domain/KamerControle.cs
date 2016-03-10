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
        public bool AllesGedaan { get; set; }

        public DateTime Datum { get; set; }

        public KamerControle()
        {
            KamerControleItems = new List<KamerControleItem>();
        }

        public KamerControle(DateTime datum)
        {
            Datum = datum;
            KamerControleItems = new List<KamerControleItem>();
            AllesGedaan = IsAllesGedaan();
            AllesInOrde = IsAllesInOrde();
        }

        public void AddKamerControleItem(KamerControleItem item)
        {
            KamerControleItems.Add(item);
        }

        public bool IsAllesGedaan()
        {
            var counter = 0;
            foreach (var i in KamerControleItems)
            {
                if (i.OpdrachtGedaan)
                {
                    counter++;
                }
            }
            return KamerControleItems.Count == counter;
        }

        public bool IsAllesInOrde()
        {
            var counter = 0;
            foreach (var i in KamerControleItems)
            {
                if (i.OpdrachtGedaan && i.OpdrachtGedaanControle)
                {
                    counter++;
                }
            }
            return KamerControleItems.Count == counter;
        }
       

    }
}