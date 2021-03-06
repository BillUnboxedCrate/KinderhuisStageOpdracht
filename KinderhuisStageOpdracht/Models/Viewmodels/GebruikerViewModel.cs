﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
using Foolproof;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class GebruikerViewModel
    {
        //Gebruikers
        #region Gebruiker
        public class OpvoederEnClientListViewModel
        {
            public OpvoederListViewModel Olmv { get; set; }
            public ClientListViewModel Clvm { get; set; }
            public LeefgroepListViewModel Llvm { get; set; }

            public OpvoederEnClientListViewModel(OpvoederListViewModel olvm, ClientListViewModel clvm, LeefgroepListViewModel llvm)
            {
                Olmv = olvm;
                Clvm = clvm;
                Llvm = llvm;
            }
        }

        public class LeefgroepListViewModel
        {
            public List<LeefgroepViewModel> List { get; set; }

            public LeefgroepListViewModel()
            {
                List = new List<LeefgroepViewModel>();
            }

            public void AddLeefgroep(LeefgroepViewModel item)
            {
                List.Add(item);
            }

        }

        public class LeefgroepViewModel
        {
            public int Id { get; set; }
            [Required]
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Naam { get; set; }
            [Required]
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Straat { get; set; }
            [Required]
            [StringLength(5, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string StraatNummer { get; set; }
            [Required]
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Gemeente { get; set; }
            [Required]
            [RegularExpression("/^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$/i", ErrorMessage = "Dit is geen correcte postcode")]
            public string Postcode { get; set; }

            public string Adres { get; set; }

            public LeefgroepViewModel() { }

            public LeefgroepViewModel(int id, string naam, string adres)
            {
                Id = id;
                Naam = naam;
                Adres = adres;
            }
        }

        public class OpvoederViewModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Voornaam { get; set; }
            public string Opvangtehuis { get; set; }
            public string ImageUrl { get; set; }
            public bool IsStagiair { get; set; }

            public OpvoederViewModel()
            {
            }

            public OpvoederViewModel(int id, string voornaam, string fullname, string imageUrl, bool nothing)
            {
                Id = id;
                Voornaam = voornaam;
                FullName = fullname;
                ImageUrl = imageUrl;
            }

            public OpvoederViewModel(int id, string fullname, string opvangtehuis, bool isStagiair)
            {
                Id = id;
                FullName = fullname;
                Opvangtehuis = opvangtehuis;
                IsStagiair = isStagiair;
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
            public string FullName { get; set; }
            public string Voornaam { get; set; }
            public string Opvangtehuis { get; set; }
            public string ImageUrl { get; set; }

            public ClientViewModel()
            {
            }

            public ClientViewModel(int id, string fullname, string voornaam, string imageUrl, bool nothing)
            {
                Id = id;
                Voornaam = voornaam;
                FullName = fullname;
                ImageUrl = imageUrl;
            }

            public ClientViewModel(int id, string fullname, string opvangtehuis)
            {
                Id = id;
                FullName = fullname;
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
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            [StringLength(50, ErrorMessage = "De voornaam kan maar {0} karakters zijn.")]
            public string Voornaam { get; set; }

            [Required]
            [Display(Name = "Gebruikersnaam")]
            [StringLength(50, ErrorMessage = "De gebruikersnaam kan maar {0} karakters zijn.")]
            public string GebruikersNaam { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Wachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [System.ComponentModel.DataAnnotations.Compare("Wachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }

            [Display(Name = "Kies een leefgroep")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            [Display(Name = "Is deze persoon een stagiair")]
            public bool IsStagair { get; set; }

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
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            [StringLength(50, ErrorMessage = "De voornaam kan maar {0} karakters zijn.")]
            public string Voornaam { get; set; }

            [Required]
            [Display(Name = "Gebruikersnaam")]
            [StringLength(50, ErrorMessage = "De gebruikersnaam kan maar {0} karakters zijn.")]
            public string GebruikersNaam { get; set; }

            //public string Wachtwoord { get; set; }

            [Display(Name = "Kies een leefgroep")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }
        }

        public class CreateClientViewModel
        {
            public string GebruikerType { get; set; }

            [Required]
            [Display(Name = "Naam")]
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            [StringLength(50, ErrorMessage = "De voornaam kan maar {0} karakters zijn.")]
            public string Voornaam { get; set; }

            [Required]
            [Display(Name = "Gebruikersnaam")]
            [StringLength(50, ErrorMessage = "De gebruikersnaam kan maar {0} karakters zijn.")]
            public string GebruikersNaam { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Wachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [System.ComponentModel.DataAnnotations.Compare("Wachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigWachtwoord { get; set; }

            [Display(Name = "Kies een leefgroep")]
            public List<string> Opvangtehuizen { get; set; }

            [Display(Name = "Kies een foto")]
            [DataType(DataType.Upload)]
            public HttpPostedFileBase ImageUpload { get; set; }

            [Required]
            public string GeselecteerdOpvangtehuisId { get; set; }

            public CreateClientViewModel()
            {
                Opvangtehuizen = new List<string>();
            }

            public CreateClientViewModel(string gebruikerType)
            {
                GebruikerType = gebruikerType;
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
            [StringLength(50, ErrorMessage = "De naam kan maar {0} karakters zijn.")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            [StringLength(50, ErrorMessage = "De voornaam kan maar {0} karakters zijn.")]
            public string Voornaam { get; set; }

            [Required]
            [Display(Name = "Gebruikersnaam")]
            [StringLength(50, ErrorMessage = "De gebruikersnaam kan maar {0} karakters zijn.")]
            public string GebruikersNaam { get; set; }

            //public string Wachtwoord { get; set; }

            [Display(Name = "Kies een leefgroep")]
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

            [Display(Name = "Gebruikersnaam")]
            public string GebruikersNaam { get; set; }

            public string LeefgroepNaam { get; set; }
            public string LeefgroepAdres { get; set; }
            public string LeefgroepGemeente { get; set; }

            [Display(Name = "Kies een foto")]
            [DataType(DataType.Upload)]
            public HttpPostedFileBase Image { get; set; }

            public string ImageUrl { get; set; }

            public string TypeGebruiker { get; set; }

            public bool IsStagair { get; set; }

            public List<TimeTrackerViewModel> TimeTrackerList { get; set; }

            public List<string> Clients { get; set; }

            public DetailViewModel()
            {
                Sancties = new List<SanctieViewModel>();
                TimeTrackerList = new List<TimeTrackerViewModel>();
                Clients = new List<string>();
            }

            public DetailViewModel(int id, string naam, string voornaam, string gebruikersnaam,
                string leefgroepNaam, string leefgroepAdres, string leefgroepGemeente,
                string typeGebruiker, string imageUrl)
            {
                Id = id;
                Naam = naam;
                Voornaam = voornaam;
                GebruikersNaam = gebruikersnaam;

                LeefgroepNaam = leefgroepNaam;
                LeefgroepAdres = leefgroepAdres;
                LeefgroepGemeente = leefgroepGemeente;

                TypeGebruiker = typeGebruiker;
                ImageUrl = imageUrl;
                IsStagair = false;
                Sancties = new List<SanctieViewModel>();
                TimeTrackerList = new List<TimeTrackerViewModel>();
                Clients = new List<string>();
            }

            public List<SanctieViewModel> Sancties { get; set; }

            public void AddSanctie(SanctieViewModel svm)
            {
                Sancties.Add(svm);
            }

            public void AddTimeTrack(TimeTrackerViewModel item)
            {
                TimeTrackerList.Add(item);
            }

            public void AddClient(string item)
            {
                Clients.Add(item);
            }

        }

        #region TimeTracker
        public class TimeTrackerViewModel
        {
            public DateTime TimeTrackTime { get; set; }

            public TimeTrackerViewModel(DateTime timeTrackTime)
            {
                TimeTrackTime = timeTrackTime;
            }
        }
        #endregion

        public class EditViewModel
        {
            public int Id { get; set; }
            public string TypeGebruiker { get; set; }

            [Required]
            [Display(Name = "Naam")]
            public string Naam { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            public string ImageUrl { get; set; }

            public HttpPostedFileBase Image { get; set; }

            //public string Wachtwoord { get; set; }

            [Display(Name = "Kies een leefgroep")]
            public List<string> Opvangtehuizen { get; set; }

            public string GeselecteerdOpvangtehuisId { get; set; }

            public EditViewModel()
            {
                Opvangtehuizen = new List<string>();
            }

            public EditViewModel(int id, string naam, string voornaam,
                 string opvangtehuis, string typegebruiker, string imageUrl)
            {
                Id = id;
                Naam = naam;
                Voornaam = voornaam;
                GeselecteerdOpvangtehuisId = opvangtehuis;
                TypeGebruiker = typegebruiker;
                ImageUrl = imageUrl;
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
        #endregion

        //Forum
        #region Forum
        public class ForumViewModel
        {
            public int ForumId { get; set; }
            public List<PostViewModel> List { get; set; }
            public string Post { get; set; }
            public string OntvangerNaam { get; set; }
            public string TypeGebruiker { get; set; }

            public ForumViewModel()
            {
                List = new List<PostViewModel>();
            }

            public ForumViewModel(int id, string type, string ontvangernaam)
            {
                ForumId = id;
                TypeGebruiker = type;
                OntvangerNaam = ontvangernaam;
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
            public string ImageUrl { get; set; }
            public bool Mine { get; set; }
            public string Boodschap { get; set; }

            public PostViewModel() { }

            public PostViewModel(int id, string sendby, DateTime timestamp, string boodschap, bool mine, string imageUrl)
            {
                Id = id;
                SendBy = sendby;
                TimeStamp = timestamp;
                Boodschap = boodschap;
                Mine = mine;
                ImageUrl = imageUrl;
            }
        }

        #endregion


        //Sancties
        #region Sanctie
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

            [Display(Name = "Cliënt")]
            public string Client { get; set; }

            [Required(ErrorMessage = "Er moet een reden worden opgegeven.")]
            [Display(Name = "Reden voor de beloning/sanctie")]
            public string Rede { get; set; }

            public DateTime Today { get; set; }

            [Required]
            [Display(Name = "Begindatum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [GreaterThanOrEqualTo("Today", ErrorMessage = "De beloning/straf kan niet voor vandaag beginnen")]
            public DateTime Date { get; set; }
            public string BeginDateDay { get; set; }

            [Required]
            [Display(Name = "Einddatum")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [GreaterThanOrEqualTo("Date", ErrorMessage = "De einddatum mag niet voor de begindatum komen")]
            public DateTime EindDatum { get; set; }
            public string EndDateDay { get; set; }

            [Required]
            [Display(Name = "Aantal dagen")]
            public int AantalDagen { get; set; }

            [Display(Name = "Kies een beloning/sanctie")]
            public List<string> Straffen { get; set; }
            public string GeselecteerdeStraf { get; set; }

            public bool StrafOfBeloning { get; set; }

            public string ImageUrl { get; set; }

            public SanctieViewModel()
            {
                Today = DateTime.Today;
                Straffen = new List<string>();
            }

            public SanctieViewModel(int id, string client)
            {
                Id = id;
                Client = client;
            }


            public SanctieViewModel(string rede, DateTime begindatum, DateTime eindatum, string straf)
            {
                Today = DateTime.Today;
                Rede = rede;
                Date = begindatum;
                EindDatum = eindatum;
                GeselecteerdeStraf = straf;
                BeginDateDay = GetDayOfWeek(Date);
                EndDateDay = GetDayOfWeek(EindDatum);

            }

            public SanctieViewModel(string client, string rede, DateTime begindatum, DateTime eindatum, string straf)
            {
                Today = DateTime.Today;
                Client = client;
                Rede = rede;
                Date = begindatum;
                EindDatum = eindatum;
                GeselecteerdeStraf = straf;
                BeginDateDay = GetDayOfWeek(Date);
                EndDateDay = GetDayOfWeek(EindDatum);
            }

            public SanctieViewModel(string rede, DateTime begindatum, DateTime eindatum, string straf, string imageUrl, bool strafofbeloning)
            {
                Today = DateTime.Today;
                Rede = rede;
                Date = begindatum;
                EindDatum = eindatum;
                GeselecteerdeStraf = straf;
                ImageUrl = imageUrl;
                BeginDateDay = GetDayOfWeek(Date);
                EndDateDay = GetDayOfWeek(EindDatum);
                StrafOfBeloning = strafofbeloning;
            }

            public void AddStraf(string straf)
            {
                Straffen.Add(straf);
            }

            public void SetStraffen(List<string> straffen)
            {
                Straffen = straffen;
            }

            private string GetDayOfWeek(DateTime date)
            {
                var culture = new CultureInfo("nl-NL");
                var dag = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dag);
            }
        }
        #endregion


        //Kamercontrole
        #region Kamercontrole

        public class KamerControleClientViewModel
        {
            public ListKamerControleItemsViewmodel ListKamerControleItemsViewmodel { get; set; }
            public KamerControleListIndexViewModel KamerControleListIndexViewModel { get; set; }

            public KamerControleClientViewModel() { }

            public KamerControleClientViewModel(ListKamerControleItemsViewmodel model1,
                KamerControleListIndexViewModel model2)
            {
                ListKamerControleItemsViewmodel = model1;
                KamerControleListIndexViewModel = model2;
            }
        }

        public class ListKamerControleItemsViewmodel
        {
            public List<KamerControleItemViewModel> KamerControleItems { get; set; }
            public KamerControleListIndexViewModel KamerControleListIndexViewModel { get; set; }

            public ListKamerControleItemsViewmodel()
            {
                KamerControleItems = new List<KamerControleItemViewModel>();
            }

            public void AddKamerControleItem(KamerControleItemViewModel item)
            {
                KamerControleItems.Add(item);
            }
        }



        public class KamerControleListIndexViewModel
        {
            public int ClientId { get; set; }
            public string ClientName { get; set; }

            public List<KamerControleIndexViewModel> List { get; set; }

            public KamerControleListIndexViewModel()
            {
                List = new List<KamerControleIndexViewModel>();
            }

            public KamerControleListIndexViewModel(int clientId, string clientName)
            {
                ClientId = clientId;
                ClientName = clientName;
                List = new List<KamerControleIndexViewModel>();
            }

            public KamerControleListIndexViewModel(int clientId)
            {
                ClientId = clientId;
                List = new List<KamerControleIndexViewModel>();
            }

            public void AddKamerControleIndexItem(KamerControleIndexViewModel item)
            {
                List.Add(item);
            }
        }

        public class KamerControleIndexViewModel
        {

            public int Id { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime Datum { get; set; }
            public string DatumDay { get; set; }
            public bool AllesGedaan { get; set; }
            public bool InOrde { get; set; }

            public KamerControleIndexViewModel() { }

            public KamerControleIndexViewModel(int id, DateTime datum, bool inOrde)
            {
                Id = id;
                Datum = datum;
                InOrde = inOrde;
                DatumDay = GetDayOfWeek(datum);
            }

            private string GetDayOfWeek(DateTime date)
            {
                var culture = new CultureInfo("nl-NL");
                var dag = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dag);
            }
        }



        public class KamerControleOverviewViewModel
        {
            public List<string> Titels { get; set; }
            public List<KamerControleViewModel> KamerControleViewModels { get; set; }

            public KamerControleOverviewViewModel()
            {
                Titels = new List<string>();
                KamerControleViewModels = new List<KamerControleViewModel>();
            }


            public void AddTitles(string item)
            {
                Titels.Add(item);
            }

            public void AddKamercontroleViewModel(KamerControleViewModel item)
            {
                KamerControleViewModels.Add(item);
            }
        }

        public class KamerControleViewModel
        {
            public int ClientId { get; set; }
            public string Client { get; set; }
            public List<KamerControleItemViewModel> ControleItemViewModels { get; set; }

            public KamerControleViewModel()
            {
                ControleItemViewModels = new List<KamerControleItemViewModel>();
            }

            public KamerControleViewModel(int clientId, string client)
            {
                ClientId = clientId;
                Client = client;
                ControleItemViewModels = new List<KamerControleItemViewModel>();
            }

            public void AddKamerControleItemViewModel(KamerControleItemViewModel item)
            {
                ControleItemViewModels.Add(item);
            }
        }

        public class KamerControleItemViewModel
        {
            public string ImageUrl { get; set; }
            public string Titel { get; set; }
            public bool DoneOpvoeder { get; set; }
            public string Uitleg { get; set; }

            public KamerControleItemViewModel() { }

            public KamerControleItemViewModel(string imageUrl, string titel, bool doneOpvoeder, string uitleg)
            {
                ImageUrl = imageUrl;
                Titel = titel;
                DoneOpvoeder = doneOpvoeder;
                Uitleg = uitleg;
            }

            public KamerControleItemViewModel(string titel, bool doneOpvoeder, string uitleg)
            {
                Titel = titel;
                DoneOpvoeder = doneOpvoeder;
                Uitleg = uitleg;
            }

            public KamerControleItemViewModel(string titel, bool doneOpvoeder)
            {
                Titel = titel;
                DoneOpvoeder = doneOpvoeder;
            }

        }
        #endregion

        #region Instellingen
        public class InstellingenViewModel
        {
            public string GebruikerType { get; set; }

            public ProfielAfbeeldingInstellenViewModel ProfielAfbeeldingInstellenViewModel { get; set; }
            public AchtergrondAfbeeldingInstellenViewModel AchtergrondAfbeeldingInstellenViewModel { get; set; }
            public WachtwoordChangeViewModel ChangeViewModel { get; set; }

            public InstellingenViewModel() { }

            public InstellingenViewModel(string gebruikerType, ProfielAfbeeldingInstellenViewModel profiel, AchtergrondAfbeeldingInstellenViewModel achtergrond)
            {
                GebruikerType = gebruikerType;
                ProfielAfbeeldingInstellenViewModel = profiel;
                AchtergrondAfbeeldingInstellenViewModel = achtergrond;
            }

            public InstellingenViewModel(ProfielAfbeeldingInstellenViewModel profiel, AchtergrondAfbeeldingInstellenViewModel achtergrond)
            {
                ProfielAfbeeldingInstellenViewModel = profiel;
                AchtergrondAfbeeldingInstellenViewModel = achtergrond;
            }


        }

        public class ProfielAfbeeldingInstellenViewModel
        {
            public string ProfielAfbeeldingUrl { get; set; }
            [DataType(DataType.Upload)]
            public HttpPostedFileBase ProfielAfbeeldingUpload { get; set; }

            public ProfielAfbeeldingInstellenViewModel() { }

            public ProfielAfbeeldingInstellenViewModel(string url)
            {
                ProfielAfbeeldingUrl = url;
            }


        }

        public class AchtergrondAfbeeldingInstellenViewModel
        {
            public string BackgroundUrl { get; set; }
            [DataType(DataType.Upload)]
            public HttpPostedFileBase BackgroundUpload { get; set; }

            public AchtergrondAfbeeldingInstellenViewModel() { }

            public AchtergrondAfbeeldingInstellenViewModel(string backgroundUrl)
            {
                BackgroundUrl = backgroundUrl;
            }
        }



        public class WachtwoordChangeViewModel
        {
            [Required]
            [Display(Name = "Huidig wachtwoord")]
            [DataType(DataType.Password)]
            public string Wachtwoord { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Nieuw Wachtwoord")]
            public string NieuwWachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("NieuwWachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigNieuwWachtwoord { get; set; }

            public WachtwoordChangeViewModel() { }
        }

        public class ForgottenPasswordChangeViewModel
        {
            public int Id { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Nieuw Wachtwoord")]
            public string NieuwWachtwoord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("NieuwWachtwoord",
                ErrorMessage = "Het wachtwoord en bevestig wachtwoord komen niet overeen")]
            public string BevestigNieuwWachtwoord { get; set; }

            public ForgottenPasswordChangeViewModel() { }

            public ForgottenPasswordChangeViewModel(int id)
            {
                Id = id;
            }
        }
        #endregion


    }
}