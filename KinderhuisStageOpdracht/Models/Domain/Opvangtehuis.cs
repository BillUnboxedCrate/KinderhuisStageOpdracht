using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvangtehuis
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
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

        public List<Suggestie> GetSuggesties()
        {
            return Suggesties.OrderByDescending(s => s.TimeStamp).ToList();

        }

        public List<Menu> GetMenus()
        {
            return Menus.OrderByDescending(m => m.BegindagWeek).ToList();
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

        public void DeleteSuggestion(Suggestie suggestie)
        {
            Suggesties.Remove(suggestie);
        }

        public void DeleteSuggestie(int id)
        {
            var suggestie = Suggesties.FirstOrDefault(s => s.Id == id);
            Suggesties.Remove(suggestie);
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

        public void RemoveMenu(Menu menu)
        {
            Menus.Remove(menu);
        }

        public void RemoveMenu(int id)
        {
            var menu = Menus.FirstOrDefault(m => m.Id == id);
            Menus.Remove(menu);
        }

        public void AddKlacht(Klacht klacht)
        {
            Klachten.Add(klacht);
        }

        public void AddKlacht(string title, string beschrijving, Client client)
        {
            var klacht = new Klacht()
            {
                Beschrijving = beschrijving,
                Client = client,
                Titel = title,
                TimeStamp = DateTime.Now
            };

            Klachten.Add(klacht);
        }

        public void AddStraf(Straf straf)
        {
            Straffen.Add(straf);
        }

        public void AddStraf(string naam, string imageUrl)
        {
            Straffen.Add(new Straf(naam, imageUrl));
        }

        public List<Straf> GetStraffen()
        {
            return Straffen.OrderBy(s => s.Id).ToList();
        }

        public Straf FindStrafByName(string name)
        {
            return Straffen.FirstOrDefault(s => s.Naam == name);
        }

        public void AddOpdrachten(KamerControleOpdracht opdracht)
        {
            Opdrachten.Add(opdracht);
        }

        public void AddOpdrachten(string titel, string beschrijving, string imageUrl)
        {
            Opdrachten.Add(new KamerControleOpdracht(titel, beschrijving, imageUrl));
        }

        public List<KamerControleOpdracht> GetKamerControleOpdrachten()
        {
            return Opdrachten.OrderBy(k => k.Titel).ToList();
        } 

        public Menu GetMenuVanDeWeek()
        {
            var menu = Menus.FirstOrDefault(m => m.BegindagWeek <= DateTime.Today || m.EinddagWeek >= DateTime.Today);
            return menu;
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1} {2}\n{3} {4}", Naam, Straat, StraatNummer, Postcode, Gemeente);
        }

       
    }
}