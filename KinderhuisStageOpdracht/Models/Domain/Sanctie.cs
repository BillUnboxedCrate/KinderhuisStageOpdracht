using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Sanctie
    {
        public int Id { get; set; }
        public bool Verboden { get; set; }
        public string Genre { get; set; }
        public string Rede { get; set; }
        public DateTime Datum { get; set; }

        public Sanctie() { }

        public Sanctie(bool verboden, string genre, string rede, DateTime datum)
        {
            Verboden = verboden;
            Genre = genre;
            Rede = rede;
            Datum = datum;
        }

    }
}