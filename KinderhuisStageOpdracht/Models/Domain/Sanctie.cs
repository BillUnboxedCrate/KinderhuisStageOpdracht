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
        public string Genre { get; set; }
        public string Rede { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        
        [NotMapped]
        public int AantalDagen { get; set; }

        public Sanctie() { }

        public Sanctie(string genre, string rede, DateTime datum, int aantalDagen)
        {
            Genre = genre;
            Rede = rede;
            BeginDatum = datum;
            EindDatum = datum.AddDays(aantalDagen - 1);
        }

    }
}