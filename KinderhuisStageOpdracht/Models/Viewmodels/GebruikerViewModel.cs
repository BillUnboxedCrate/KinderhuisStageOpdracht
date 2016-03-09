﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class GebruikerViewModel
    {
        public class OpvoederEnClientListViewModel
        {
            public OpvoederListViewModel Olmv { get; set; }
            public ClientListViewModel Clvm { get; set; }

            public OpvoederEnClientListViewModel(OpvoederListViewModel olvm, ClientListViewModel clvm)
            {
                Olmv = olvm;
                Clvm = clvm;
            }
        }

        public class OpvoederViewModel
        {
            public int Id { get; set; }
            public string Naam { get; set; }
            public string Email { get; set; }
            public string Opvangtehuis { get; set; }

            public OpvoederViewModel()
            {
            }

            public OpvoederViewModel(int id, string naam, string email)
            {
                Id = id;
                Naam = naam;
                Email = email;
            }

            public OpvoederViewModel(int id, string naam, string email, string opvangtehuis)
            {
                Id = id;
                Naam = naam;
                Email = email;
                Opvangtehuis = opvangtehuis;
            }
        }

        public class OpvoederListViewModel
        {
            public List<OpvoederViewModel> Opvoeders { get; set; }

            public OpvoederListViewModel()
            {
                Opvoeders = new List<OpvoederViewModel>();
            }

            public void AddOpvoeder(OpvoederViewModel opvoeder)
            {
                Opvoeders.Add(opvoeder);
            }
        }

        public class ClientViewModel
        {
            public int Id { get; set; }
            public string Naam { get; set; }
            public string Email { get; set; }
            public string Opvangtehuis { get; set; }

            public ClientViewModel()
            {
            }

            public ClientViewModel(int id, string naam, string email)
            {
                Id = id;
                Naam = naam;
                Email = email;
            }

            public ClientViewModel(int id, string naam, string email, string opvangtehuis)
            {
                Id = id;
                Naam = naam;
                Email = email;
                Opvangtehuis = opvangtehuis;
            }
        }

        public class ClientListViewModel
        {
            public List<ClientViewModel> Clients { get; set; }

            public ClientListViewModel()
            {
                Clients = new List<ClientViewModel>();
            }

            public void AddClient(ClientViewModel client)
            {
                Clients.Add(client);
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
            [System.ComponentModel.DataAnnotations.Compare("Wachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            public CreateOpvoederViewModel()
            {
            }

            public CreateOpvoederViewModel(List<string> opvangtehuizen)
            {
                Opvangtehuizen = opvangtehuizen;
            }

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
            [System.ComponentModel.DataAnnotations.Compare("Wachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }

            [Display(Name = "Kies een opvangtehuis")]
            public List<string> Opvangtehuizen { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            [Required]
            public string GeselecteerdOpvangtehuisId { get; set; }

            public CreateClientViewModel()
            {
                Opvangtehuizen = new List<string>();
            }

            public CreateClientViewModel(List<string> opvangtehuizen)
            {
                Opvangtehuizen = opvangtehuizen;
            }

            public void AddOpvangtehuis(string opvangtehuis)
            {
                Opvangtehuizen.Add(opvangtehuis);
            }

            public void SetOpvangtehuizen(List<string> opvangtehuizen)
            {
                Opvangtehuizen = opvangtehuizen;
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
            [Display(Name = "Email adres")]
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
            public DateTime? GeboorteDatum { get; set; }

            [Display(Name = "Gebruikers naam")]
            public string GebruikersNaam { get; set; }

            [Display(Name = "Email adres")]
            public string Email { get; set; }

            [Display(Name = "Opvangtehuis")]
            public string Opvangtehuis { get; set; }

            public DetailViewModel()
            {
                Sancties = new List<SanctieViewModel>();
            }

            public DetailViewModel(int id, string naam, string voornaam, DateTime? geboortedatum, string gebruikersnaam,
                string email, string opvangtehuis)
            {
                Id = id;
                Naam = naam;
                Voornaam = voornaam;
                GeboorteDatum = geboortedatum;
                GebruikersNaam = gebruikersnaam;
                Email = email;
                Opvangtehuis = opvangtehuis;
                Sancties = new List<SanctieViewModel>();
            }

            public List<SanctieViewModel> Sancties { get; set; }

            public void AddSanctie(SanctieViewModel svm)
            {
                Sancties.Add(svm);
            }

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
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

            public EditViewModel()
            {
                Opvangtehuizen = new List<string>();
            }

            public EditViewModel(int id, string naam, string voornaam, DateTime geboortedatum, string gebruikersnaam,
                string email, string opvangtehuis)
            {
                Id = id;
                Naam = naam;
                Voornaam = voornaam;
                GeboorteDatum = geboortedatum;
                GebruikersNaam = gebruikersnaam;
                Email = email;
                GeselecteerdOpvangtehuisId = opvangtehuis;
                Opvangtehuizen = new List<string>();
            }

            public void AddOpvangtehuis(string opvangtehuis)
            {
                Opvangtehuizen.Add(opvangtehuis);
            }

            public void SetOpvangtehuizen(List<string> opvangtehuizen)
            {
                Opvangtehuizen = opvangtehuizen;
            }


        }

        public class ForumListViewModel
        {
            public List<ForumViewModel> List { get; set; }

            public ForumListViewModel()
            {
                List = new List<ForumViewModel>();
            }


            public void AddForum(ForumViewModel forum)
            {
                List.Add(forum);
            }
        }

        public class ForumViewModel
        {
            public int Id { get; set; }
            public string Client { get; set; }

            public ForumViewModel()
            {
            }

            public ForumViewModel(int id)
            {
                Id = id;
            }
        }

        public class PostsListViewModel
        {
            public List<PostViewModel> List { get; set; }

            public PostsListViewModel()
            {
                List = new List<PostViewModel>();
            }

            public void AddPost(PostViewModel post)
            {
                List.Add(post);
            }
        }

        public class PostViewModel
        {
            public int Id { get; set; }
            public string SendBy { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Boodschap { get; set; }

            public PostViewModel()
            {
            }

            public PostViewModel(string sendby, DateTime timestamp, string boodschap)
            {
                SendBy = sendby;
                TimeStamp = timestamp;
                Boodschap = boodschap;
            }
        }

        public class SanctieListViewModel 
        {
            public List<SanctieViewModel> SanctieList { get; set; }

            public SanctieListViewModel()
            {
                SanctieList = new List<SanctieViewModel>();
            }

            public void AddSanctie(SanctieViewModel sanctie)
            {
                SanctieList.Add(sanctie);
            }
        }

        public class SanctieViewModel
        {
            public int Id { get; set; }

            public string Client { get; set; }

            [Required]
            [Display(Name = "Rede voor de sanctie")]
            public string Rede { get; set; }

            [Required]
            [Display(Name = "Selecteer een datum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime Date { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime EindDatum { get; set; }

            [Required]
            [Display(Name = "Aantal dagen")]
            public int AantalDagen { get; set; }

            [Display(Name = "Kies een straf")]
            public List<string> Straffen { get; set; }
            public string GeselecteerdeStraf { get; set; }

            public string ImageUrl { get; set; }

            public SanctieViewModel()
            {
                Straffen = new List<string>();
            }

            public SanctieViewModel(int id, string client)
            {
                Id = id;
                Client = client;
            }

            public SanctieViewModel(string rede, DateTime begindatum, DateTime eindatum, string straf)
            {
                Rede = rede;
                Date = begindatum;
                EindDatum = eindatum;
                GeselecteerdeStraf = straf;
            }

            public SanctieViewModel(string rede, DateTime begindatum, DateTime eindatum, string straf, string imageUrl)
            {
                Rede = rede;
                Date = begindatum;
                EindDatum = eindatum;
                GeselecteerdeStraf = straf;
                ImageUrl = imageUrl;
            }

            public void AddStraf(string straf)
            {
                Straffen.Add(straf);
            }

            public void SetStraffen(List<string> straffen)
            {
                Straffen = straffen;
            }
        }
    }
}