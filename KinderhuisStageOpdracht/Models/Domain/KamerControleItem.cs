using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerControleItem
    {
        public int Id { get; set; }

        public virtual KamerControleOpdracht KamerControleOpdracht { get; set; }

        public bool OpdrachtGedaanControle { get; set; }
        public string Uitleg { get; set; }

        public KamerControleItem() { }

        public KamerControleItem(KamerControleOpdracht kamerControleOpdracht)
        {
            KamerControleOpdracht = kamerControleOpdracht;
            OpdrachtGedaanControle = false;
        }

        public string GetControleOpdrachtImageUrl()
        {
            return KamerControleOpdracht.ImageUrl;
        }

        public string GetControleOpdrachtTitel()
        {
            return KamerControleOpdracht.Titel;
        }

        public string GetControleOpdrachtBeschrijving()
        {
            return KamerControleOpdracht.Beschrijving;
        }
    }

}