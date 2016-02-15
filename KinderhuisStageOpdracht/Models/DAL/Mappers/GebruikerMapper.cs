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

            //Foreign Key
        }
    }
}