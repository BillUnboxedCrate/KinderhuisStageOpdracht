using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Menu
    {
        public int Id { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}