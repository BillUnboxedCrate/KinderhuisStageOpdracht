using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNet.Identity;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public abstract class Gebruiker
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        [NotMapped]
        public string PlainWachtwoord { get; set; }
        public string Salt { get; set; }
        public bool Visable { get; set; }

        public virtual ICollection<Taak> Taken { get; set; }
        public virtual Opvangtehuis Opvangtehuis { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.ImageUrl)]
        public string BackgroundUrl { get; set; }

        /*private string _salt;
        public string Salt
        {
            get
            {
                return _salt;
            }
            set
            {
                _salt = GenerateSalt();
            }
        }*/

        protected Gebruiker()
        {
            Taken = new List<Taak>();
            Visable = true;
            //Salt = GenerateSalt();
        }


        private string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[1024];

            rng.GetBytes(buffer);
            var generatedSalt = BitConverter.ToString(buffer);
            /*var stringBuilder = new StringBuilder(Voornaam);
            stringBuilder.Append(Naam);
            stringBuilder.Append(GeboorteDatum.Date.Year);

            var salt = stringBuilder.ToString();*/

            return generatedSalt;
        }

        public string GiveFullName()
        {
            return Voornaam + " " + Naam;
        }

        public string GetOpvangtehuisnaam()
        {
            return Opvangtehuis.Naam;
        }

        public string GetOpvangtehuis()
        {
            return Opvangtehuis.ToString();
        }

        public void EditGebruiker(string naam, string voornaam, Opvangtehuis opvangtehuis, string gebruikersnaam,
             string imageUrl)
        {
            Naam = naam;
            Voornaam = voornaam;
            Opvangtehuis = opvangtehuis;
            Gebruikersnaam = gebruikersnaam;

            if (imageUrl != "~/Content/Images/ProfielAfbeelding/dafault.png" || imageUrl != "~/Content/Images/ProfielAfbeelding/dafaultAvatar.png")
            {
                ImageUrl = imageUrl;
            }

        }

        public void DeleteGebruiker()
        {
            Visable = false;
        }

        //Instellingen
        public void AddImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }


        public void AddBackground(string backgroundUrl)
        {
            BackgroundUrl = backgroundUrl;
        }

        public void WachtwoordAanpassen(string nieuwWachtwoord, string salt)
        {
            Wachtwoord = nieuwWachtwoord;
            Salt = salt;
        }
    }
}