using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Client : Gebruiker
    {
        public virtual ICollection<PlanningItem> PlanningItems { get; set; }

        public virtual ICollection<KamerControle> KamerControles { get; set; }

        public virtual ICollection<Forum> Forums { get; set; }

        public virtual ICollection<Sanctie> Sancties { get; set; }

        public virtual ICollection<TimeTrack> TimeTrackList { get; set; }

        public Client()
        {
            KamerControles = new List<KamerControle>();
            Forums = new List<Forum>();
            Sancties = new List<Sanctie>();
            TimeTrackList = new List<TimeTrack>();
            PlanningItems = new List<PlanningItem>();
        }

        public Client(string naam, string voornaam, Opvangtehuis opvangtehuis, string gebruikersnaam, string wachtwoord, string salt)
        {
            Naam = naam;
            Voornaam = voornaam;
            Opvangtehuis = opvangtehuis;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Salt = salt;
            ImageUrl = "~/Content/Images/ProfielAfbeelding/defaultAvatar.png";
        }

        public void AddKamerControle(KamerControle kamerControle)
        {
            KamerControles.Add(kamerControle);
        }

        private bool DagelijkseKamerControleAlGemaakt()
        {
            if (KamerControles.FirstOrDefault(k => k.Datum == DateTime.Today) != null)
            {
                return true;
            }
            return false;
        }

        //Kamercontrole
        public KamerControle GetTodaysKamerControle()
        {
            return KamerControles.FirstOrDefault(k => k.Datum == DateTime.Today);
        }

        public KamerControle ViewKamerControle(List<KamerControleOpdracht> opdrachts)
        {
            if (!DagelijkseKamerControleAlGemaakt())
            {
                var kamerControle = new KamerControle(DateTime.Today);

                return CreateKamerControleList(kamerControle, opdrachts);
            }

            return KamerControles.FirstOrDefault(k => k.Datum == DateTime.Today);
        }

        private KamerControle CreateKamerControleList(KamerControle kamerControle, List<KamerControleOpdracht> opdrachts)
        {
            foreach (var o in opdrachts)
            {
                kamerControle.AddKamerControleItem(new KamerControleItem(o));
            }
            AddKamerControle(kamerControle);

            return kamerControle;
        }

        public KamerControle GetKamerControleById(int id)
        {
            return KamerControles.FirstOrDefault(i => i.Id == id);
        }

        public List<KamerControle> GetKamerControles()
        {
            return KamerControles.OrderByDescending(k => k.Datum).Take(7).ToList();
        }


        //Sancties
        public void AddSanctie(Sanctie sanctie)
        {
            Sancties.Add(sanctie);
        }

        public void AddSanctie(string rede, DateTime datum, int aantalDagen, Straf straf)
        {
            var sanctie = new Sanctie(rede, datum, aantalDagen, straf);

            Sancties.Add(sanctie);
        }

        public List<Sanctie> GetAppliedSancties()
        {
            return Sancties.Where(s => s.EindDatum >= DateTime.Today).OrderBy(s => s.BeginDatum).ToList();
        }

        public List<Sanctie> GetSancties()
        {
            return Sancties.OrderByDescending(s => s.BeginDatum).ToList();
        }


        //Forum/Messaging/Blog/etc
        private bool IsForumAlGemaakt(int clientId, int opvoederId)
        {
            var forum = Forums.FirstOrDefault(f => f.Client.Id == clientId && f.Opvoeder.Id == opvoederId);

            return forum != null;
        }

        public Forum GetForumById(int id)
        {
            return Forums.FirstOrDefault(f => f.Id == id);
        }

        public void AddForum(Forum forum)
        {
            Forums.Add(forum);
        }

        public Forum GetForum(Opvoeder opvoeder, Client client)
        {
            if (!IsForumAlGemaakt(client.Id, opvoeder.Id))
            {
                var forum = new Forum(opvoeder, client);
                Forums.Add(forum);
                opvoeder.AddForum(forum);
                return forum;
            }
            return Forums.FirstOrDefault(f => f.Client == client && f.Opvoeder == opvoeder);
        }

        //Planning
        public List<PlanningItem> GetPlanning()
        {
            return PlanningItems.Where(p => p.Datum >= DateTime.Today).OrderBy(p => p.Datum).ToList();
        }

        public void AddPlanning(DateTime datum, string activiteit)
        {
            PlanningItems.Add(new PlanningItem(activiteit, datum));
        }

        public void RemovePlanning(int id)
        {
            var item = PlanningItems.FirstOrDefault(p => p.Id == id);
            PlanningItems.Remove(item);
        }

        //TimeTracking
        public List<TimeTrack> GetTimeTrackList()
        {
            return TimeTrackList.OrderBy(t => t.Aanmelden).ToList();
        } 

        public void AddTimeTrack()
        {
            var timetrack = new TimeTrack();
            LoginTrack(timetrack);
            TimeTrackList.Add(timetrack);
        }

        private void LoginTrack(TimeTrack timeTrack)
        {
            timeTrack.Aanmelden = DateTime.Now;
        }

        /*public void LogOffTrack(TimeTrack timeTrack, bool sessionVerlopen)
        {
            timeTrack.Afmelden = sessionVerlopen == false ? DateTime.Now : timeTrack.Aanmelden.AddMinutes(20);
        }*/
    }
}