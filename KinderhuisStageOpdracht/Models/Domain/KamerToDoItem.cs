using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerToDoItem
    {
        public int Id { get; set; }
        public KamerToDo KamerToDo { get; set; }
    }
}