﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class KamerToDoItemMapper: EntityTypeConfiguration<KamerToDoItem>
    {
        public KamerToDoItemMapper()
        {
            //Primary Key
            HasKey(ktdi => ktdi.Id);

            //Properties

            //Foreign Key
        }
       
    }
}