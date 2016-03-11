﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Client : Gebruiker
    {
        public virtual ICollection<Planning> Plannings { get; set; }
        public virtual ICollection<KamerControle> KamerControles { get; set; }

        public virtual ICollection<Forum> Forums { get; set; }

        public virtual ICollection<Sanctie> Sancties { get; set; }

        public Client()
        {
            Plannings = new List<Planning>();
            KamerControles = new List<KamerControle>();
            Forums = new List<Forum>();
            Sancties = new List<Sanctie>();
        }

        public Client(string naam, string voornaam, Opvangtehuis opvangtehuis, string gebruikersnaam, string email, string wachtwoord, string salt, DateTime geboortedatum)
        {
            Naam = naam;
            Voornaam = voornaam;
            Opvangtehuis = opvangtehuis;
            Gebruikersnaam = gebruikersnaam;
            Email = email;
            Wachtwoord = wachtwoord;
            Salt = salt;
            GeboorteDatum = geboortedatum;
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
            return KamerControles.OrderByDescending(k => k.Datum).ToList();
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
    }
}