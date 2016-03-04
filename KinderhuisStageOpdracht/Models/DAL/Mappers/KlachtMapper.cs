using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KlachtMapper : EntityTypeConfiguration<Klacht>
    {
        public KlachtMapper()
        {
            //Primary Key
            HasKey(k => k.Id);

            //Property
            Property(k => k.Titel).IsRequired();
            Property(k => k.Beschrijving).IsRequired();
            Property(k => k.TimeStamp).IsRequired();
            
            //Foreign key
            HasRequired(k => k.Client).WithMany().WillCascadeOnDelete(true);
        }
    }
}