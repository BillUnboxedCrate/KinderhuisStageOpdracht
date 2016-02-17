using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public abstract class Gebruiker
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        [NotMapped]
        public string PlainWachtwoord { get; set; }
        public virtual ICollection<Taak> Taken { get; set; }

        private string _salt;
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
        }

        protected Gebruiker()
        {
            Taken = new List<Taak>();
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


    }
}