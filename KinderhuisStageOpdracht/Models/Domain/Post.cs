using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string Boodschap { get; set; }
        public virtual Gebruiker Gebruiker { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}