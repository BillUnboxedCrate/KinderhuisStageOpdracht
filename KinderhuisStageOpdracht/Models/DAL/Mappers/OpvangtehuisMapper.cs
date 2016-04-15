using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class OpvangtehuisMapper:EntityTypeConfiguration<Opvangtehuis>
    {
        public OpvangtehuisMapper()
        {
            //Primary Key
            HasKey(oh => oh.Id);

            //Property
            Property(oh => oh.Naam).IsRequired().HasMaxLength(50);
            Property(oh => oh.Straat).IsRequired().HasMaxLength(50);
            Property(oh => oh.StraatNummer).IsRequired().HasMaxLength(5);
            Property(oh => oh.Gemeente).IsRequired().HasMaxLength(50);
            Property(oh => oh.Postcode).IsRequired().HasMaxLength(4);

            //Foreign Key
            //HasMany(oh => oh.Menus).WithRequired().WillCascadeOnDelete(true);
            //HasMany(oh => oh.Opdrachten).WithRequired().WillCascadeOnDelete(true);
            //HasMany(oh => oh.Straffen).WithRequired().WillCascadeOnDelete(true);
        }

    }
}