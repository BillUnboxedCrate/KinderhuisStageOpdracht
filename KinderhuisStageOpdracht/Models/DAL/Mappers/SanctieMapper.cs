﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class SanctieMapper : EntityTypeConfiguration<Sanctie>
    {
        public SanctieMapper()
        {
            //Primary Key
            HasKey(s => s.Id);

            //Property
            Property(s => s.BeginDatum).IsRequired();

            //Foreign Key
            HasRequired(s => s.Straf).WithMany();
        }
    }
}