using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class TaakMapper : EntityTypeConfiguration<Taak>
    {
        public TaakMapper()
        {
            //Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Titel).IsRequired().HasMaxLength(50);
            Property(t => t.Beschrijving).IsRequired();

            //Foreign Key
        }
    }
}