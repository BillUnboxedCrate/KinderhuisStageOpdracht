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
            Property(pi => pi.Titel).IsRequired().HasMaxLength(50);
            Property(pi => pi.Omschrijving).IsRequired();

            //Foreign Key
        }   
    }
}