using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class OpvoederMapper : EntityTypeConfiguration<Opvoeder>
    {
        public OpvoederMapper()
        {
            //Primary Key

            //Property
            //Property(o => o.Email).IsRequired().HasMaxLength(50);

            //Foreign Key
            HasMany(o => o.Forums).WithRequired().WillCascadeOnDelete(true);

        }
    }
}