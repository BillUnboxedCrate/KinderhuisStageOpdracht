﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.DAL.Mappers;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ProjectContext:DbContext
    {
        public DbSet<Gebruiker> GebruikerSet { get; set; }
        public DbSet<Menu> MenuSet { get; set; }
        public DbSet<Taak> TaakSet { get; set; } 

        public ProjectContext()
            : base("kinderhuisconnectionstring")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GebruikerMapper());



            modelBuilder.Configurations.Add(new KamerToDoItemMapper());
            modelBuilder.Configurations.Add(new KamerToDoMapper());
            modelBuilder.Configurations.Add(new MenuItemMapper());
            modelBuilder.Configurations.Add(new MenuMapper());
            modelBuilder.Configurations.Add(new PlanningItemMapper());
            modelBuilder.Configurations.Add(new PlanningMapper());
            modelBuilder.Configurations.Add(new TaakMapper());
        }
    }
}