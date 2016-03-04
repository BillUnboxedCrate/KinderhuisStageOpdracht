using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class PostMapper : EntityTypeConfiguration<Post>
    {
        public PostMapper()
        {
            //Primary id
            HasKey(p => p.Id);

            //Property
            Property(p => p.Boodschap).IsRequired();
            Property(p => p.TimeStamp).IsRequired();

            //Foreign Key
            HasRequired(p => p.Gebruiker).WithMany().WillCascadeOnDelete(true);
        }
    }
}