using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class MenuItemMapper : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMapper()
        {
            //Primary Key
            HasKey(mi => mi.Id);

            //Properties
            Property(mi => mi.Voorgerecht).IsRequired();
            Property(mi => mi.Hoofdgerecht).IsRequired();
            Property(mi => mi.Dessert).IsRequired();
            Property(mi => mi.Dag).IsRequired();
            Property(mi => mi.Datum).IsRequired();

            //Foreign Key
        }  
    }
}