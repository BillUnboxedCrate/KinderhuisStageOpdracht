using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class PlanningMapper : EntityTypeConfiguration<Planning>
    {
        public PlanningMapper()
        {
            //Primary Key
            HasKey(p => p.Id);

            //Properties

            //Foreign Key
            //HasRequired(p => p.Client).WithRequiredPrincipal().Map(p => p.MapKey("ClientId"));
            //HasMany(p => p.PlanningItems).WithRequired().Map(p => p.MapKey("PlanningId")).WillCascadeOnDelete(true);
            HasMany(p => p.PlanningItems).WithRequired().WillCascadeOnDelete(true);
        }  
    }
}