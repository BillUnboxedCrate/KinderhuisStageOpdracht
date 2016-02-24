using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class OpvangtehuisViewModel
    {
        public class SuggestieListViewModel
        {
            public List<SuggestieViewModel> Suggesties { get; set; }

            public SuggestieListViewModel()
            {
                Suggesties = new List<SuggestieViewModel>();
            }
        }

        public class SuggestieViewModel
        {
            public int Id { get; set; }


            [Display(Name = "Aangemaakt op")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
            public DateTime TimeStamp { get; set; }

            [Display(Name = "Genre")]
            public string Genre { get; set; }

            [Display(Name = "Gesuggereerd door")]
            public string Client { get; set; }

            [Display(Name = "Suggestie")]
            public string Beschrijving { get; set; }
        }

        public class CreateSuggestieViewModel
        {
            [Required]
            public string Beschrijving { get; set; }

            public string GeselecteerdGenre { get; set; }

            public ICollection<SelectListItem> Genres
            {
                get
                {
                    return new[]
                    {   
                        new SelectListItem {Text = "Suggestie voor eten", Value = "Eten"},
                        new SelectListItem {Text = "Suggestie voor een activiteit", Value = "Activiteit"}
                    };
                }
            }
        }
   
    }
}