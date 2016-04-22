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
            public StrafIndexViewModel StrafIndexViewModel { get; set; }

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
            [Display(Name = "Naam voor de beloning/straf")]
            public string Naam { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            [Required]
            [Display(Name = "Is dit een beloning of straf")]
            public bool StrafOfBeloning { get; set; }

            public string ImageUrl { get; set; }

            public StrafIndexViewModel() { }

            public StrafIndexViewModel(int id, string imageUrl, string naam, bool strafofbeloning)
            {
                Id = id;
                ImageUrl = imageUrl;
                Naam = naam;
                StrafOfBeloning = strafofbeloning;
            }
        }
    }
}