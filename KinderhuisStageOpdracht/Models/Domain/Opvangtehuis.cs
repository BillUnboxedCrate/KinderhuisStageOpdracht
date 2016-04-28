using System;
using System.Collections.Generic;
using System.Linq;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvangtehuis
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string StraatNummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode { get; set; }

        public virtual ICollection<Suggestie> Suggesties { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<KamerControleOpdracht> Opdrachten { get; set; }
        public virtual ICollection<Straf> Straffen { get; set; }

        public virtual ICollection<Klacht> Klachten { get; set; }

        public Opvangtehuis()
        {
            Suggesties = new List<Suggestie>();
            Menus = new List<Menu>();
            Klachten = new List<Klacht>();
            Straffen = new List<Straf>();
            Opdrachten = new List<KamerControleOpdracht>();

        }

        public Opvangtehuis(string naam, string straat, string straatnummer, string gemeente, string postcode)
        {
            Naam = naam;
            Straat = straat;
            StraatNummer = straatnummer;
            Gemeente = gemeente;
            Postcode = postcode;
        }


        //Suggesties
        public List<Suggestie> GetSuggesties()
        {
            return Suggesties.OrderByDescending(s => s.TimeStamp).ToList();

        }

        public void AddSuggestie(Suggestie suggestie)
        {
            Suggesties.Add(suggestie);
        }

        public void AddSuggestie(string beschrijving, string genre, Client client)
        {
            var s = new Suggestie()
            {
                Beschrijving = beschrijving,
                Genre = genre,
                Client = client,
                TimeStamp = DateTime.Now
            };
            Suggesties.Add(s);

        }

        public Suggestie FindSuggestieById(int id)
        {
            return Suggesties.FirstOrDefault(s => s.Id == id);
        }

        public void DeleteSuggestie(int id)
        {
            var suggestie = Suggesties.FirstOrDefault(s => s.Id == id);
            Suggesties.Remove(suggestie);
        }


        //Menus
        public List<Menu> GetMenus()
        {
            return Menus.OrderByDescending(m => m.BegindagWeek).ToList();
        }

        public void AddMenu(DateTime begindag)
        {
            var menu = new Menu
            {
                BegindagWeek = begindag
            };
            Menus.Add(menu);
        }

        public Menu FindMenuByDate(DateTime date)
        {
            return Menus.FirstOrDefault(m => m.BegindagWeek == date);
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
        }

        public void DeleteMenu(int id)
        {
            var menu = Menus.FirstOrDefault(m => m.Id == id);
            Menus.Remove(menu);
        }

        public Menu GetMenuVanDeWeek()
        {
            var menu = Menus.FirstOrDefault(m => m.BegindagWeek <= DateTime.Today || m.EinddagWeek >= DateTime.Today);
            return menu;
        }

        //Klachten
        public List<Klacht> GetKlachten()
        {
            return Klachten.OrderByDescending(k => k.TimeStamp).ToList();
        }

        public void AddKlacht(Klacht klacht)
        {
            Klachten.Add(klacht);
        }

        public void AddKlacht(string omschrijving, Client client)
        {
            var klacht = new Klacht(omschrijving, client);
            Klachten.Add(klacht);
        }

        public void DeleteKlacht(int id)
        {
            var klacht = Klachten.FirstOrDefault(k => k.Id == id);
            Klachten.Remove(klacht);
        }


        //Straffen
        public void AddStraf(Straf straf)
        {
            Straffen.Add(straf);
        }

        public void AddStraf(string naam, bool strafofbeloning)
        {
            Straffen.Add(new Straf(naam, strafofbeloning));
        }

        public void RemoveStraf(int id)
        {
            var straf = Straffen.FirstOrDefault(s => s.Id == id);
            Straffen.Remove(straf);
        }

        public List<Straf> GetStraffen()
        {
            return Straffen.OrderBy(s => s.Id).ToList();
        }

        public Straf FindStrafByName(string name)
        {
            return Straffen.FirstOrDefault(s => s.Naam == name);
        }


        //Kamercontrole oprachten
        public void AddOpdracht(KamerControleOpdracht opdracht)
        {
            Opdrachten.Add(opdracht);
        }

        public KamerControleOpdracht FindOpdrachById(int id)
        {
            return Opdrachten.FirstOrDefault(o => o.Id == id);
        }

        public void AddOpdrachten(string titel, string imageUrl)
        {
            Opdrachten.Add(new KamerControleOpdracht(titel, imageUrl));
        }

        public List<KamerControleOpdracht> GetKamerControleOpdrachten()
        {
            return Opdrachten.OrderBy(k => k.Titel).ToList();
        }

        public void RemoveOpdracht(int id)
        {
            var opdracht = Opdrachten.FirstOrDefault(o => o.Id == id);
            Opdrachten.Remove(opdracht);
        }

        //Return string
        public override string ToString()
        {
            return String.Format("{0}\n{1} {2}\n{3} {4}", Naam, Straat, StraatNummer, Postcode, Gemeente);
        }

        


    }
}