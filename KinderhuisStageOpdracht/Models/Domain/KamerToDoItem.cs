﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class KamerToDoItem
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public bool Gedaan { get; set; }
    }
}