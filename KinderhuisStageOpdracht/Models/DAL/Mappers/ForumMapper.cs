using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class ForumMapper : EntityTypeConfiguration<Forum>
    {
        public ForumMapper()
        {
            //Primary id
            HasKey(f => f.Id);

            //Property

            //Foreign Key
            HasMany(f => f.Posts).WithRequired().WillCascadeOnDelete(true);
        }
       
    }
}