using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerControleOpdrachtMapper: EntityTypeConfiguration<KamerControleOpdracht>
    {
        public KamerControleOpdrachtMapper()
        {
            //Primary Key
            HasKey(kco => kco.Id);

            //Properties
            Property(kco => kco.Titel).IsRequired().HasMaxLength(50);
            Property(kco => kco.Beschrijving).IsRequired();

            //Foreign Key
        }
       
    }
}