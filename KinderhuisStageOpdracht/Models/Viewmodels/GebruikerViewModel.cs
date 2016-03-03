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
            public string Naam { get; set; }
            public string Email { get; set; }
            public string Opvangtehuis { get; set; }
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
            public string Naam { get; set; }
            public string Email { get; set; }
            public string Opvangtehuis { get; set; }
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

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }

        }

        public class EditOpvoederViewModel
        {
            public int Id { get; set; }

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

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }
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
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "Geboorte datum")]
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

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            [Required]
            public string GeselecteerdOpvangtehuisId { get; set; }

            public CreateClientViewModel()
            {
                Opvangtehuizen = new List<string>();
            }

            
        }
        
        public class EditClientViewModel
        {
            public int Id { get; set; }

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

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }
        }

       
        public class DetailViewModel
        {
            [Display(Name = "Gebruikers id")]
            public int Id { get; set; }

            [Display(Name = "Naam")]
            public string Naam { get; set; }

            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime GeboorteDatum { get; set; }

            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }

            [Display(Name = "Email adres")]
            public string Email { get; set; }

            [Display(Name = "Opvangtehuis")]
            public string Opvangtehuis { get; set; }
        }

        public class EditViewModel
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboorte datum")]
            [DisplayFormat(DataFormatString = "{0:d}")]
            public DateTime GeboorteDatum { get; set; }

            [Required]
            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }

            [EmailAddress]
            [Display(Name = "Email adres")]
            public string Email { get; set; }
            //public string Wachtwoord { get; set; }

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }
        }

        public class ForumListViewModel
        {
            public List<ForumViewModel> List { get; set; }

            public ForumListViewModel()
            {
                List = new List<ForumViewModel>();
            }
        }

        public class ForumViewModel
        {
            public int Id { get; set; }
            public string Client { get; set; }

        }

        public class PostsListViewModel
        {
            public List<PostViewModel> List { get; set; }

            public PostsListViewModel()
            {
                List = new List<PostViewModel>();
            }
        }

        public class PostViewModel
        {
            public int Id { get; set; }
            public string SendBy { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Boodschap { get; set; }
        }
    }
}