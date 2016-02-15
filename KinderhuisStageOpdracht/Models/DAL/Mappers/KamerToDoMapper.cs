using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerToDoMapper : EntityTypeConfiguration<KamerToDo>
    {
        public KamerToDoMapper()
        {
            //Primary Key
            HasKey(ktd => ktd.Id);

            //Properties

            //Foreign Key
        } 
    }
}