using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual ICollection<Taak> Taken { get; set; }

        public string Salt { get; set; }

        protected Gebruiker()
        {
            Taken = new List<Taak>();
            Salt = GenerateSalt();
        }


        private string GenerateSalt()
        {
            var stringBuilder = new StringBuilder(Voornaam);
            stringBuilder.Append(Naam);
            //stringBuilder.Append(GeboorteDatum.Date.Year);

            var salt = stringBuilder.ToString();

            return salt.ToLower();
        }


    }
}