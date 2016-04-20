using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class PlanningItemMapper : EntityTypeConfiguration<PlanningItem>
    {
        public PlanningItemMapper()
        {
            //Primary Key
            HasKey(pi => pi.Id);

            //Properties
            Property(pi => pi.Actie).IsRequired();
            Property(pi => pi.Datum).IsRequired();

            //Foreign Key
        }   
    }
}