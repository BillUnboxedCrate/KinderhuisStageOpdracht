using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Forum
    {
        public int Id { get; set; }
        public virtual Opvoeder Opvoeder { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Forum()
        {
            Posts = new List<Post>();
        }

        public Forum(Opvoeder opvoeder, Client client)
        {
            Opvoeder = opvoeder;
            Client = client;    
            Posts = new List<Post>();
        }

        public void AddPost(string boodschap, Gebruiker gebruiker)
        {
            var post = new Post(boodschap, gebruiker);
            
            Posts.Add(post);
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
        }
    }
}