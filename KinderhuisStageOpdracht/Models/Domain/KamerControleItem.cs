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

        public bool OpdrachtGedaan { get; set; }
        public bool OpdrachtGedaanControle { get; set; }
    }
}