using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class ProjectInitializer : DropCreateDatabaseAlways<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {
            var admin1 = new Admin()
            {
                Naam = "Baert",
                Voornaam = "Bram",
                GeboorteDatum = new DateTime(1993,11,16),
                Gebruikersnaam = "Admin",
                Wachtwoord = "test"
                
            };

            var opvoeder1 = new Opvoeder()
            {
                Naam = "De Seager",
                Voornaam = "Tom",
                GeboorteDatum = new DateTime(1990,11,11),
                Gebruikersnaam = "Opvoeder",
                Wachtwoord = "test"
            };

            var client1 = new Client()
            {
                Naam = "Bauwens",
                Voornaam = "Brikke",
                GeboorteDatum = new DateTime(1991, 12, 5),
                Gebruikersnaam = "BrikkeBauwens",
                Wachtwoord = "test"
            };

            context.GebruikerSet.Add(admin1);
            context.GebruikerSet.Add(opvoeder1);
            context.GebruikerSet.Add(client1);

            admin1.Clients.Add(client1);
            admin1.Opvoeders.Add(opvoeder1);

            opvoeder1.Clients.Add(client1);

            context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Database created!");
        }
    }
}