﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class ProjectInitializer : CreateDatabaseIfNotExists<ProjectContext>
    {
        enum Dagen
        {
            Maandag,
            Dinsdag,
            Woensdag,
            Donderdag,
            Vrijdag,
            Zaterdag,
            Zondag
        }

        protected override void Seed(ProjectContext context)
        {
            #region opvangtehuizen

            var opvangtehuis1 = new Opvangtehuis()
            {
                Naam = "Kinderhuis Sint Vincentius",
                Gemeente = "Dendermonde",
                Postcode = "9200",
                Straat = "Leopold II laan",
                StraatNummer = "6",
            };

            var opvangtehuis2 = new Opvangtehuis()
            {
                Naam = "De Hermelijn",
                Gemeente = "Hamme",
                Postcode = "9220",
                Straat = "Jagerstraat",
                StraatNummer = "32",
            };

            //Add opvangtehuizen
            context.OpvangtehuisSet.Add(opvangtehuis1);
            context.OpvangtehuisSet.Add(opvangtehuis2);
            #endregion

            #region gebruikers
            //Admins
            var admin1 = new Admin()
            {
                Naam = "Baert",
                Voornaam = "Bram",
                Gebruikersnaam = "Admin",
                //Wachtwoord = "test",
                //testing
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis1,
                ImageUrl = "/Content/Images/ProfielAfbeelding/default.png"

            };

            //Opvoeders
            //Leefgroep kinderhuis
            var opvoeder2 = new Opvoeder()
            {
                Naam = "Braeckman",
                Voornaam = "Yannick",
                Gebruikersnaam = "YannickBraeckman",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis1,
                ImageUrl = "/Content/Images/ProfielAfbeelding/default.png"
            };


            //Leefgroep Hermelijn
            var opvoeder1 = new Opvoeder()
            {
                Naam = "De Seager",
                Voornaam = "Tom",
                Gebruikersnaam = "Opvoeder",
                //Wachtwoord = "hSnuFWINbUy6RcorgcO6yzbXxqvTNxsxg8G59Q3MduePwlhnRdjzRXZlsljumUJ/PfkEd+QezJDwpvRB6F14Ug==",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/tomds.jpg"
            };


            var opvoeder3 = new Opvoeder()
            {
                Naam = "Michiels",
                Voornaam = "Sandra",
                Gebruikersnaam = "Sandra",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/Sandra.jpg"
            };

            var opvoeder4 = new Opvoeder()
            {
                Naam = "Cattelain",
                Voornaam = "Julie",
                Gebruikersnaam = "Julie",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/Julie.jpg"
            };

            var opvoeder5 = new Opvoeder()
            {
                Naam = "Gysens",
                Voornaam = "Anneliese",
                Gebruikersnaam = "Anneliese",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/default.png"
            };

            var opvoeder6 = new Opvoeder()
            {
                Naam = "Hamerlinck",
                Voornaam = "Magda",
                Gebruikersnaam = "Magda",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/default.png"
            };

            var opvoeder7 = new Opvoeder()
            {
                Naam = "Jacobs",
                Voornaam = "Candy",
                Gebruikersnaam = "Candy",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/Candy.jpg"
            };

            var opvoeder8 = new Opvoeder()
            {
                Naam = "Lejeune",
                Voornaam = "Alexander",
                Gebruikersnaam = "Alexander",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/alex.jpg"
            };

            var opvoeder9 = new Opvoeder()
            {
                Naam = "Spruyt",
                Voornaam = "Martine",
                Gebruikersnaam = "Martine",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/Martine.jpg"
            };

            var opvoeder10 = new Opvoeder()
            {
                Naam = "Verstuyft",
                Voornaam = "Tom",
                Gebruikersnaam = "Tom",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/tomv.jpg"
            };

            //Clients
            var client1 = new Client()
            {
                Naam = "Bauwens",
                Voornaam = "Brikke",
                Gebruikersnaam = "BrikkeBauwens",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/defaultAvatar.png"
            };

            var client2 = new Client()
            {
                Naam = "Hollanders",
                Voornaam = "Roy",
                Gebruikersnaam = "RoyHollanders",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/defaultAvatar.png"
            };

            var client3 = new Client()
            {
                Naam = "Van Den Berghe",
                Voornaam = "Rutger",
                Gebruikersnaam = "RutgerVanDenBerghe",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis2,
                ImageUrl = "/Content/Images/ProfielAfbeelding/defaultAvatar.png",
                BackgroundUrl = "/Content/Images/Backgrounds/foto-alta-risoluzione-91.jpg"
            };

            var client4 = new Client()
            {
                Naam = "Braeckman",
                Voornaam = "Sean",
                Gebruikersnaam = "SeanBraeckman",
                Wachtwoord = "$2a$10$GRgbctIhj9/kkzr.VPiH/.IDOE5CwwpmbbwcV8T4ex7nEOlOhQLzO",
                Salt = "100000.tPEvKWuP2095wwvwwedVj0InJATX3Zqh49l8itxrhYpIuQ==",
                Opvangtehuis = opvangtehuis1,
                ImageUrl = "/Content/Images/ProfielAfbeelding/defaultAvatar.png"
            };

            //Add admins
            context.GebruikerSet.Add(admin1);

            //Add opvoerders
            //In opvangtehuis 1
            context.GebruikerSet.Add(opvoeder2);

            //In opvangtehuis 2
            context.GebruikerSet.Add(opvoeder1);
            context.GebruikerSet.Add(opvoeder3);
            context.GebruikerSet.Add(opvoeder4);
            context.GebruikerSet.Add(opvoeder5);
            context.GebruikerSet.Add(opvoeder6);
            context.GebruikerSet.Add(opvoeder7);
            context.GebruikerSet.Add(opvoeder8);
            context.GebruikerSet.Add(opvoeder9);
            context.GebruikerSet.Add(opvoeder10);


            //Add clients 
            //In opvangtehuis 1
            context.GebruikerSet.Add(client4);

            //In opvangtehuis 2
            context.GebruikerSet.Add(client1);
            context.GebruikerSet.Add(client2);
            context.GebruikerSet.Add(client3);

            #endregion

            #region Straf

            var straf1 = new Straf("Geen frisdrank", true);

            var straf2 = new Straf("Geen spelletjes", true);

            var straf3 = new Straf("Geen frisdrank", true);

            var straf4 = new Straf("Geen spelletjes", true);

            var beloning1 = new Straf("Half uurtje langer opblijven", false);
            var beloning2 = new Straf("Half uurtje langer opblijven", false);


            opvangtehuis1.Straffen.Add(straf1);
            opvangtehuis1.Straffen.Add(straf2);
            opvangtehuis1.Straffen.Add(beloning1);

            opvangtehuis2.Straffen.Add(straf3);
            opvangtehuis2.Straffen.Add(straf4);
            opvangtehuis2.Straffen.Add(beloning2);
            #endregion

            #region menus
            var menu1 = new Menu(new DateTime(2016, 2, 29));

            var menu2 = new Menu(new DateTime(2016, 2, 22));

            #region menuitems
            var menuitem1 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 15),
                Dag = Dagen.Maandag.ToString(),
                Voorgerecht = "Soep",
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
                Datum = new DateTime(2016, 2, 20),
                Dag = Dagen.Zaterdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem7 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 21),
                Dag = Dagen.Zondag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };



            var menuitem8 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 22),
                Dag = Dagen.Maandag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem9 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 23),
                Dag = Dagen.Dinsdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem10 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 24),
                Dag = Dagen.Woensdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem11 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 25),
                Dag = Dagen.Donderdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem12 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 26),
                Dag = Dagen.Vrijdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem13 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 27),
                Dag = Dagen.Zaterdag.ToString(),
                Voorgerecht = "Soep",
                Hoofdgerecht = "Eten",
                Dessert = "Dessert"
            };

            var menuitem14 = new MenuItem()
            {
                Datum = new DateTime(2016, 2, 28),
                Dag = Dagen.Zondag.ToString(),
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
                DatumTijd = new DateTime(2016, 2, 18, 14, 0, 0)
            };

            var taak2 = new Taak()
            {
                Titel = "Keuken",
                Beschrijving = "Afwas doen",
                DatumTijd = new DateTime(2016, 2, 17, 12, 0, 0)
            };
            #endregion



            #region KamerControleOpdracht
            var kamercontroleOpracht1 = new KamerControleOpdracht()
            {
                Titel = "Bed opmaken",
                ImageUrl = "~/Content/Images/KamerControleImages/bed_opmaken.png"
            };

            var kamercontroleOpracht2 = new KamerControleOpdracht()
            {
                Titel = "Kleren opruimen",
                ImageUrl = "~/Content/Images/KamerControleImages/was_opruimen.png"
            };

            var kamercontroleOpracht3 = new KamerControleOpdracht()
            {
                Titel = "Gordijnen openen",
                ImageUrl = "~/Content/Images/KamerControleImages/gordijn_openen.jpg"
            };

            var kamercontroleOpracht4 = new KamerControleOpdracht()
            {
                Titel = "Bed opmaken",
                ImageUrl = "~/Content/Images/KamerControleImages/bed_opmaken.png"
            };

            var kamercontroleOpracht5 = new KamerControleOpdracht()
            {
                Titel = "Kleren opruimen",
                ImageUrl = "~/Content/Images/KamerControleImages/was_opruimen.png"
            };

            var kamercontroleOpracht6 = new KamerControleOpdracht()
            {
                Titel = "Gordijnen openen",
                ImageUrl = "~/Content/Images/KamerControleImages/gordijn_openen.jpg"
            };

            opvangtehuis1.Opdrachten.Add(kamercontroleOpracht1);
            opvangtehuis1.Opdrachten.Add(kamercontroleOpracht2);
            opvangtehuis1.Opdrachten.Add(kamercontroleOpracht3);

            opvangtehuis2.Opdrachten.Add(kamercontroleOpracht4);
            opvangtehuis2.Opdrachten.Add(kamercontroleOpracht5);
            opvangtehuis2.Opdrachten.Add(kamercontroleOpracht6);

            #endregion

            #region forum
            var forum1 = new Forum();

            var forum2 = new Forum();
            #endregion

            #region posts
            var post1 = new Post()
            {
                Boodschap = "Testpost",
                Gebruiker = opvoeder1,
                TimeStamp = DateTime.Now
            };
            #endregion

            forum1.AddPost(post1);

            //Add forum
            opvoeder1.AddForum(forum1);
            client1.AddForum(forum1);

            opvoeder2.AddForum(forum2);
            client4.AddForum(forum2);

            //Add menus
            menu1.MenuItems.Add(menuitem1);
            menu1.MenuItems.Add(menuitem2);
            menu1.MenuItems.Add(menuitem3);
            menu1.MenuItems.Add(menuitem4);
            menu1.MenuItems.Add(menuitem5);
            menu1.MenuItems.Add(menuitem6);
            menu1.MenuItems.Add(menuitem7);

            menu2.MenuItems.Add(menuitem8);
            menu2.MenuItems.Add(menuitem9);
            menu2.MenuItems.Add(menuitem10);
            menu2.MenuItems.Add(menuitem11);
            menu2.MenuItems.Add(menuitem12);
            menu2.MenuItems.Add(menuitem13);
            menu2.MenuItems.Add(menuitem14);

            opvangtehuis2.Menus.Add(menu1);
            opvangtehuis2.Menus.Add(menu2);


            //Add taken
            //taak1.Gebruikers.Add(client1);
            //taak2.Gebruikers.Add(client1);
            //taak1.Gebruikers.Add(client3);
            //taak1.Gebruikers.Add(client4);


            context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Database created!");
        }
    }
}