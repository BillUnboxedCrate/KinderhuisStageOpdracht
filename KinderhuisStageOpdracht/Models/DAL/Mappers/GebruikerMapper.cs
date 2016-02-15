using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class GebruikerMapper : EntityTypeConfiguration<Gebruiker>
    {
        public GebruikerMapper()
        {
            //Primary Key
            HasKey(g => g.Id);

            //Property
            Property(g => g.Naam).IsRequired().HasMaxLength(50);
            Property(g => g.Voornaam).IsRequired().HasMaxLength(50);
            Property(g => g.GeboorteDatum).IsRequired();

            //Foreign Key
        }
    }
}