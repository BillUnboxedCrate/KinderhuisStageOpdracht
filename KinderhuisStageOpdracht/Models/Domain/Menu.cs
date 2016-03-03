using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public class Menu
    {
        public int Id { get; set; }
        public DateTime BegindagWeek { get; set; }
        public DateTime EinddagWeek { get; set; }
        public int Week { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }

        public Menu(DateTime begindag)
        {
            BegindagWeek = begindag;
            EinddagWeek = GetEindeVanDeWeekDatum();
            Week = GetWeekVanHetJaar();
            MenuItems = new List<MenuItem>();

        }

        public void AddMenuItem(MenuItem item)
        {
            MenuItems.Add(item);
        }

        public void AddMenuItem(string dag, string dessert, string hoofdgerecht, string voorgerecht)
        {
            var item = new MenuItem()
            {
                Dag = dag,
                Voorgerecht = voorgerecht,
                Hoofdgerecht = hoofdgerecht,
                Dessert = dessert
            };

            MenuItems.Add(item);
        }

        public DateTime GetEindeVanDeWeekDatum()
        {
            return BegindagWeek.AddDays(4);
        }

        public int GetWeekVanHetJaar()
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = dfi.Calendar;
            
            return cal.GetWeekOfYear(BegindagWeek, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}