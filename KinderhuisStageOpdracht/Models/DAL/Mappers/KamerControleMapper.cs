using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerControleMapper : EntityTypeConfiguration<KamerControle>
    {
        public KamerControleMapper()
        {
            //Primary Key
            HasKey(kc => kc.Id);

            //Properties

            //Foreign Key
            //HasRequired(ktd => ktd.Client).WithRequiredPrincipal().Map(c => c.MapKey("ClientId"));
            //HasMany(ktd => ktd.KamerToDoItems).WithRequired().Map(ktd => ktd.MapKey("KamerToDoId")).WillCascadeOnDelete(true);
            
            HasMany(kc => kc.KamerControleItems).WithRequired();

        } 
    }
}