using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerControleItemMapper: EntityTypeConfiguration<KamerControleItem>
    {
        public KamerControleItemMapper()
        {
            //Primary Key
            HasKey(ktdi => ktdi.Id);

            //Properties
            Property(ktdi => ktdi.Titel).IsRequired().HasMaxLength(50);
            Property(ktdi => ktdi.Beschrijving).IsRequired();

            //Foreign Key
        }
       
    }
}