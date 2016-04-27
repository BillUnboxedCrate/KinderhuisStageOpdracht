using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class StrafMapper : EntityTypeConfiguration<Straf>
    {
        public StrafMapper()
        {
            //Primary Key
            HasKey(s => s.Id);

            //Property
            Property(s => s.Naam).IsRequired();

            //Foreign Key
        }   
    }
}