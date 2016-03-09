using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerControle
    {
        public int Id { get; set; }
        //public Opvoeder Opvoeder { get; set; }
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

        public void AddKamerToDoItem(KamerControleItem item)
        {
            KamerControleItems.Add(item);
        }

        public void AddKamerToDoItem(string titel, string beschrijving)
        {
            var item = new KamerControleItem()
            {
                Titel = titel,
                Beschrijving = beschrijving,
            };

            KamerControleItems.Add(item);
        }

    }
}