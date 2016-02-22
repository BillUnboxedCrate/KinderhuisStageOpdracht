using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class GebruikerViewModel
    {
        public class OpvoederEnClientListViewModel
        {
            public OpvoederListViewModel Olmv { get; set; }
            public ClientListViewModel Clvm { get; set; }
        }

        public class OpvoederViewModel
        {
            public int Id { get; set; }
            public string Voornaam { get; set; }
            public string Naam { get; set; }
            public string Email { get; set; }
        }

        public class OpvoederListViewModel
        {
            public List<OpvoederViewModel> Opvoeders {get; set; }

            public OpvoederListViewModel()
            {
                Opvoeders = new List<OpvoederViewModel>();
            }
        }

        public class ClientViewModel
        {
            public int Id { get; set; }
            public string Voornaam { get; set; }
            public string Naam { get; set; }
            public string Email { get; set; }
        }

        public class ClientListViewModel
        {
            public List<ClientViewModel> Clients { get; set; }

            public ClientListViewModel()
            {
                Clients = new List<ClientViewModel>();
            }
        }

        public class CreateOpvoederViewModel
        {
            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }
            
            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }
            
            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime GeboorteDatum { get; set; }
            
            [Required]
            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email adres")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Wachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("Wachtwoord", ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }
        }

        public class EditOpvoederViewModel
        {
            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }
            
            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }
            
            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime GeboorteDatum { get; set; }
            
            [Required]
            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }
            //public string Wachtwoord { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email adres")]
            public string Email { get; set; }
        }

        public class CreateClientViewModel
        {
            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }
            
            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }
            
            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime GeboorteDatum { get; set; }
            
            [Required]
            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }

            [EmailAddress]
            [Display(Name = "Email adres")]
            public string Email { get; set; }
            
            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Wachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("Wachtwoord", ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }
        }

        public class EditClientViewModel
        {
            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }
            
            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }
            
            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime GeboorteDatum { get; set; }
            
            [Required]
            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }

            [EmailAddress]
            [Display(Name="Email adres")]
            public string Email { get; set; }
            //public string Wachtwoord { get; set; }
        }
    }
}