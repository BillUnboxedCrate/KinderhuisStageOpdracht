using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Menu
    {
        public int Id { get; set; }
        public DateTime BegindagWeek { get; set; }
        public DateTime EinddagWeek { get; set; }
        public string Week { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
        {
            MenuItems.Add(item);
        }

        public void AddMenuItem(string dessert, string hoofdgerecht, string voorgerecht)
        {
            var item = new MenuItem()
            {
                Voorgerecht = voorgerecht,
                Hoofdgerecht = hoofdgerecht,
                Dessert = dessert
            };

            MenuItems.Add(item);
        }
    }
}