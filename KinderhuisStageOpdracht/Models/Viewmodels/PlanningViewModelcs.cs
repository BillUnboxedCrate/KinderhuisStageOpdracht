using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class PlanningViewModel
    {
        public class ClientPlanningListViewModel
        {
            public ClientPlanningViewModel ClientPlanningViewModel { get; set; }

            public List<ClientPlanningViewModel> PlannigList { get; set; }

            public ClientPlanningListViewModel()
            {
                PlannigList = new List<ClientPlanningViewModel>();
            }


            public void AddItem(ClientPlanningViewModel item)
            {
                PlannigList.Add(item);
            }
        }

        public class ClientPlanningViewModel
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

            public ClientPlanningViewModel() { }

            public ClientPlanningViewModel(int id, string activiteit, DateTime datum)
            {
                Id = id;
                Activiteit = activiteit;
                Datum = datum;
            }

        }
    }
}