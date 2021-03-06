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
    public class ProjectContext : DbContext
    {
        public DbSet<Gebruiker> GebruikerSet { get; set; }
        public DbSet<Opvangtehuis> OpvangtehuisSet { get; set; }

        public ProjectContext()
            : base("kinderhuisconnectionstring")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GebruikerMapper());

            modelBuilder.Configurations.Add(new ForumMapper());
            modelBuilder.Configurations.Add(new PostMapper());
            modelBuilder.Configurations.Add(new KamerControleOpdrachtMapper());
            modelBuilder.Configurations.Add(new KamerControleMapper());
            modelBuilder.Configurations.Add(new KamerControleItemMapper());
            modelBuilder.Configurations.Add(new MenuItemMapper());
            modelBuilder.Configurations.Add(new MenuMapper());
            modelBuilder.Configurations.Add(new PlanningItemMapper());
            modelBuilder.Configurations.Add(new TaakMapper());
            modelBuilder.Configurations.Add(new OpvangtehuisMapper());
            modelBuilder.Configurations.Add(new KlachtMapper());
            modelBuilder.Configurations.Add(new SanctieMapper());
            modelBuilder.Configurations.Add(new StrafMapper());
            modelBuilder.Configurations.Add(new TimeTrackMapper());
        }
    }
}