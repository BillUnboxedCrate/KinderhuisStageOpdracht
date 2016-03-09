using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerControleItemMapper : EntityTypeConfiguration<KamerControleItem>
    {
        public KamerControleItemMapper()
        {
            HasKey(kci => kci.Id);

            HasRequired(kci => kci.KamerControleOpdracht).WithMany().Map(m => m.MapKey("Opdracht_Id"));
        }
    }
}