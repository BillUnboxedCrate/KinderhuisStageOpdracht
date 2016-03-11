using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class StrafViewModel
    {
        public class StrafListIndexViewModel
        {
            public List<StrafIndexViewModel> List { get; set; }

            public StrafListIndexViewModel()
            {
                List = new List<StrafIndexViewModel>();
            }

            public void AddStrafIndexViewModel(StrafIndexViewModel item)
            {
                List.Add(item);
            }
        }

        public class StrafIndexViewModel
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Straf")]
            public string Naam { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            public StrafIndexViewModel() { }

            public StrafIndexViewModel(int id, string naam)
            {
                Id = id;
                Naam = naam;
            }
        }
    }
}