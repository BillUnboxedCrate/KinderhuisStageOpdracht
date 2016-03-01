using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Client : Gebruiker
    {
        public virtual ICollection<Planning> Plannings { get; set; }
        public virtual ICollection<KamerToDo> KamerToDos { get; set; }

        public virtual ICollection<Forum> Forums { get; set; } 

        public Client()
        {
            Plannings = new List<Planning>();
            KamerToDos = new List<KamerToDo>();
            Forums = new List<Forum>();
        }

        public void AddKamerToDo(KamerToDo toDo)
        {
            KamerToDos.Add(toDo);
        }

        public void AddForum(Forum forum)
        {
            Forums.Add(forum);
        }
    }
}