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
        public virtual ICollection<Post> Posts { get; set; }

        public Forum()
        {
            Posts = new List<Post>();
        }

        public void AddPost(string boodschap, Gebruiker gebruiker)
        {
            var post = new Post
            {
                Boodschap = boodschap,
                TimeStamp = DateTime.Now,
                Gebruiker = gebruiker
            };

            Posts.Add(post);
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
        }
    }
}