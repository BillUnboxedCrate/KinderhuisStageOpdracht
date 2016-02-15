using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.DAL.Mappers;

namespace KinderhuisStageOpdracht.Models.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ProjectContext:DbContext
    {
        public ProjectContext() : base("kinderhuisdb")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KamerToDoItemMapper());
            modelBuilder.Configurations.Add(new KamerToDoMapper());
            modelBuilder.Configurations.Add(new MenuItemMapper());
            modelBuilder.Configurations.Add(new MenuItemMapper());
            modelBuilder.Configurations.Add(new PlanningItemMapper());
            modelBuilder.Configurations.Add(new PlanningMapper());
            modelBuilder.Configurations.Add(new TaakMapper());
        }
    }
}