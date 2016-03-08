using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL.Mappers
{
    public class ClientMapper : EntityTypeConfiguration<Client>
    {
        public ClientMapper()
        {
            //Primary Key

            //Property

            //Foreign Key
            //HasRequired(c => c.Planning).WithRequiredPrincipal().Map(c => c.MapKey("PlanningId"));
            //HasRequired(c => c.KamerToDo).WithRequiredPrincipal().Map(c => c.MapKey("KamerToDoId"));
            //HasMany(c => c.Plannings).WithRequired().Map(c => c.MapKey("ClientId"));
            //HasMany(c => c.KamerToDos).WithRequired().Map(c => c.MapKey("ClientId"));

            HasMany(c => c.Plannings).WithRequired();
            HasMany(c => c.KamerToDos).WithRequired();

            HasMany(c => c.Forums).WithRequired();

            HasMany(c => c.Sancties).WithRequired();

        }
    }
}