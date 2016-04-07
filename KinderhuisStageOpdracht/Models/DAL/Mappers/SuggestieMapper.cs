using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class SuggestieMapper : EntityTypeConfiguration<Suggestie>
    {
        public SuggestieMapper()
        {
            //Primary Key
            HasKey(s => s.Id);

            //Property
            Property(s => s.Beschrijving).IsRequired();
            Property(s => s.Genre).IsRequired();
            Property(s => s.TimeStamp).IsRequired();

            //Foreign Key
            //HasRequired(s => s.Client).WithMany();
        }
    }
}