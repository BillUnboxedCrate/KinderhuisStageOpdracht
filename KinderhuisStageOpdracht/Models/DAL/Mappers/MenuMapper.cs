using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class MenuMapper : EntityTypeConfiguration<Menu>
    {
        public MenuMapper()
        {
            //Primary Key
            HasKey(m => m.Id);

            //Properties

            //Foreign Key
        }
    }
}