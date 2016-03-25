using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class TimeTrack
    {
        public int Id { get; set; }
        public DateTime Aanmelden { get; set; }
        public DateTime Afmelden { get; set; }

        public TimeTrack()
        {
            
        }


    }
}