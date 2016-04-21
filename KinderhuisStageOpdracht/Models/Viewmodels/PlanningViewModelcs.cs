using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class PlanningViewModel
    {
        public class PlanningListViewModel
        {
            public int ClientId { get; set; }
            public PlanningItemViewModel ClientPlanningViewModel { get; set; }

            public List<PlanningItemViewModel> PlannigList { get; set; }

            public PlanningListViewModel()
            {
                PlannigList = new List<PlanningItemViewModel>();
            }

            public PlanningListViewModel(int id)
            {
                ClientId = id;
                PlannigList = new List<PlanningItemViewModel>();
            }


            public void AddItem(PlanningItemViewModel item)
            {
                PlannigList.Add(item);
            }
        }

        public class PlanningItemViewModel
        {
            public int Id { get; set; }
            [Required]
            [Display(Name = "Activiteit")]
            public string Activiteit { get; set; }

            [Required]
            [Display(Name = "Datum")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime Datum { get; set; }

            public string Dag { get; set; }
            public bool Verwijderbaar { get; set; }

            public PlanningItemViewModel() { }

            public PlanningItemViewModel(int id, string activiteit, DateTime datum)
            {
                Id = id;
                Activiteit = activiteit;
                Datum = datum;
                Dag = GetDayOfWeek(Datum);
            }

            public PlanningItemViewModel(int id, string activiteit, DateTime datum, bool verwijderbaar)
            {
                Id = id;
                Activiteit = activiteit;
                Datum = datum;
                Verwijderbaar = verwijderbaar;
                Dag = GetDayOfWeek(Datum);
            }

            private string GetDayOfWeek(DateTime date)
            {
                var culture = new CultureInfo("nl-NL");
                var dag = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dag);
            }

        }
    }
}