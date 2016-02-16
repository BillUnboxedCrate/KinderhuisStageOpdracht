using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class ProjectInitializer : DropCreateDatabaseAlways<ProjectContext>
    {
        enum Dagen
        {
            Maandag,
            Dinsdag,
            Woensdag,
            Donderdag,
            Vrijdag
        }

        protected override void Seed(ProjectContext context)
        {
            #region gebruikers
            //Admins
            var admin1 = new Admin()
            {
                Naam = "Baert",
                Voornaam = "Bram",
                GeboorteDatum = new DateTime(1993,11,16),
                Gebruikersnaam = "Admin",
                Wachtwoord = "test"
                
            };

            //Opvoeders
            var opvoeder1 = new Opvoeder()
            {
                Naam = "De Seager",
                Voornaam = "Tom",
                GeboorteDatum = new DateTime(1990,11,11),
                Gebruikersnaam = "Opvoeder",
                Wachtwoord = "test"
            };

            var opvoeder2 = new Opvoeder()
            {
                Naam = "Braeckman",
                Voornaam = "Yannick",
                GeboorteDatum = new DateTime(1992,3,24),
                Gebruikersnaam = "YannickBraeckman",
                Wachtwoord = "test"
            };

            //Clients
            var client1 = new Client()
            {
                Naam = "Bauwens",
                Voornaam = "Brikke",
                GeboorteDatum = new DateTime(1991, 12, 5),
                Gebruikersnaam = "BrikkeBauwens",
                Wachtwoord = "test"
            };

            var client2 = new Client()
            {
                Naam = "Hollanders",
                Voornaam = "Roy",
                GeboorteDatum = new DateTime(1992,2,14),
                Gebruikersnaam = "RoyHollanders",
                Wachtwoord = "test"
            };

            var client3 = new Client()
            {
                Naam = "Van Den Berghe",
                Voornaam = "Rutger",
                GeboorteDatum = new DateTime(1993,9,24),
                Gebruikersnaam = "RutgerVanDenBerghe",
                Wachtwoord = "test"
            };

            var client4 = new Client()
            {
                Naam = "Braeckman",
                Voornaam = "Sean",
                GeboorteDatum = new DateTime(1992,3,25),
                Gebruikersnaam = "SeanBraeckman",
                Wachtwoord = "test"
            };
            #endregion

            //Add admins
            context.GebruikerSet.Add(admin1);

            //Add opvoerders
            context.GebruikerSet.Add(opvoeder1);
            context.GebruikerSet.Add(opvoeder2);

            //Add clients
            context.GebruikerSet.Add(client1);
            context.GebruikerSet.Add(client2);
            context.GebruikerSet.Add(client3);
            context.GebruikerSet.Add(client4);

            //Admin filling
            admin1.Clients.Add(client1);
            admin1.Clients.Add(client2);
            admin1.Clients.Add(client3);
            admin1.Clients.Add(client4);
            admin1.Opvoeders.Add(opvoeder1);
            admin1.Opvoeders.Add(opvoeder2);


            //Opvoeder filling
            opvoeder1.Clients.Add(client1);
            opvoeder1.Clients.Add(client2);
            opvoeder1.Clients.Add(client3);
            opvoeder1.Clients.Add(client4);

            opvoeder2.Clients.Add(client1);
            opvoeder2.Clients.Add(client2);
            opvoeder2.Clients.Add(client3);
            opvoeder2.Clients.Add(client4);

            //Client filling
            client1.Opvoeders.Add(opvoeder1);
            client2.Opvoeders.Add(opvoeder1);
            client3.Opvoeders.Add(opvoeder1);
            client4.Opvoeders.Add(opvoeder1);

            client1.Opvoeders.Add(opvoeder2);
            client2.Opvoeders.Add(opvoeder2);
            client3.Opvoeders.Add(opvoeder2);
            client4.Opvoeders.Add(opvoeder2);


            #region menus
            var menu1 = new Menu()
            {
                BegindagWeek = new DateTime(2016,2,15),
                EinddagWeek = new DateTime(2016,2,19)
            };

            var menu2 = new Menu()
            {
                BegindagWeek = new DateTime(2016, 2, 22),
                EinddagWeek = new DateTime(2016, 2, 26)
            };

            #region menuitems
            var menuitem1 = new MenuItem()
            {
                Datum = new DateTime(2016,2,15),
                Dag = Dagen.Maandag.ToString(),
                Voorgerecht= "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem2 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 16),
                Dag = Dagen.Dinsdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem3 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 17),
                Dag = Dagen.Woensdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem4 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 18),
                Dag = Dagen.Donderdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem5 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 19),
                Dag = Dagen.Vrijdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem6 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 22),
                Dag = Dagen.Maandag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem7 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 23),
                Dag = Dagen.Dinsdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem8 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 24),
                Dag = Dagen.Woensdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem9 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 25),
                Dag = Dagen.Donderdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem10 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 26),
                Dag = Dagen.Vrijdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };
            #endregion
            #endregion
            
            #region taken
            var taak1 = new Taak()
            {
                Titel = "Tuin",
                Beschrijving = "De tuin moet worden onderhouden",
                DatumTijd = new DateTime(2016,2,18,14,0,0)
            };

            var taak2 = new Taak()
            {
                Titel = "Keuken",
                Beschrijving = "Afwas doen",
                DatumTijd = new DateTime(2016,2,17,12,0,0)
            };
            #endregion

            #region planning

            var planning1 = new Planning()
            {
                BegindagWeek = new DateTime(2016,2,15),
                EinddagWeek = new DateTime(2016,2,21)
            };

            var planning2 = new Planning()
            {
                BegindagWeek = new DateTime(2016, 2, 15),
                EinddagWeek = new DateTime(2016, 2, 21)
            };

            var planning3 = new Planning()
            {
                BegindagWeek = new DateTime(2016, 2, 15),
                EinddagWeek = new DateTime(2016, 2, 21)
            };

            #region planningitem

            var planningitem1 = new PlanningItem()
            {

            };
            #endregion
            #endregion



            //Add menus
            context.MenuSet.Add(menu1);
            context.MenuSet.Add(menu2);

            menu1.MenuItems.Add(menuitem1);
            menu1.MenuItems.Add(menuitem2);
            menu1.MenuItems.Add(menuitem3);
            menu1.MenuItems.Add(menuitem4);
            menu1.MenuItems.Add(menuitem5);

            menu2.MenuItems.Add(menuitem6);
            menu2.MenuItems.Add(menuitem7);
            menu2.MenuItems.Add(menuitem8);
            menu2.MenuItems.Add(menuitem9);
            menu2.MenuItems.Add(menuitem10);

            //Add taken
            context.TaakSet.Add(taak1);
            context.TaakSet.Add(taak2);

            taak1.Gebruikers.Add(client1);
            taak2.Gebruikers.Add(client1);
            taak1.Gebruikers.Add(client3);
            taak1.Gebruikers.Add(client4);
            

            context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Database created!");
        }
    }
}