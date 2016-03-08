using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Sanctie
    {
        public int Id { get; set; }
        public string Rede { get; set; }

        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public virtual Straf Straf { get; set; }

        
        [NotMapped]
        public int AantalDagen { get; set; }

        public Sanctie() { }

        public Sanctie(string rede, DateTime datum, int aantalDagen)
        {
            Rede = rede;
            BeginDatum = datum;
            EindDatum = datum.AddDays(aantalDagen - 1);
        }

    }
}