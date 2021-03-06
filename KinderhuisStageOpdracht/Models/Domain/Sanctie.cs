﻿using System;
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

        public int AantalDagen { get; set; }

        public Sanctie() { }

        public Sanctie(string rede, DateTime datum, DateTime endDate, Straf straf)
        {
            Rede = rede;
            BeginDatum = datum;
            EindDatum = endDate;
            Straf = straf;
        }

        public string GetstrafNaam()
        {
            return Straf.Naam;
        }

        public string GetStrafImageUrl()
        {
            return Straf.ImageUrl;
        }

        public bool GetIfStrafOrBeloning()
        {
            return Straf.StrafOfBeloning;
        }

    }
}