using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Opvoeder : Gebruiker
    {
        public virtual ICollection<Forum> Forums { get; set; }

        public Opvoeder()
        {
            Forums = new List<Forum>();
        }

        public void AddForum(Forum forum)
        {
            Forums.Add(forum);
        }
    }
}