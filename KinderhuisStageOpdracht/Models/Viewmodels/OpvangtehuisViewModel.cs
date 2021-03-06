﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderhuisStageOpdracht.Models.Viewmodels
{
    public class OpvangtehuisViewModel
    {
        //Suggesties
        #region Suggestie
        public class SuggestieListViewModel
        {
            public List<SuggestieViewModel> Suggesties { get; set; }

            public SuggestieListViewModel()
            {
                Suggesties = new List<SuggestieViewModel>();
            }

            public void AddSuggestie(SuggestieViewModel svm)
            {
                Suggesties.Add(svm);
            }
        }

        public class SuggestieViewModel
        {
            public int Id { get; set; }

            [Display(Name = "Doorgestuurd op")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
            public DateTime? TimeStamp { get; set; }

            [Display(Name = "Genre")]
            public string Genre { get; set; }

            [Display(Name = "Gesuggereerd door")]
            public string Client { get; set; }

            [Display(Name = "Suggestie")]
            public string Beschrijving { get; set; }

            public SuggestieViewModel(DateTime? timestamp, string genre, string client, string beschrijving, int id)
            {
                Id = id;
                TimeStamp = timestamp;
                Genre = genre;
                Client = client;
                Beschrijving = beschrijving;
            }

            public SuggestieViewModel() { }
        }

        public class CreateSuggestieViewModel
        {
            [Required]
            public string Beschrijving { get; set; }

            public string GeselecteerdGenre { get; set; }
        }
        #endregion


        //Menus
        #region MenuViewModel
        public class MenuListViewModel
        {
            public List<MenuViewModel> Menus { get; set; }

            public MenuListViewModel()
            {
                Menus = new List<MenuViewModel>();
            }

            public void AddMenu(MenuViewModel mvm)
            {
                Menus.Add(mvm);
            }
        }

        //public class MenuViewModel
        //{
        //    public int Week { get; set; }

        //    [DataType(DataType.Date)]
        //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //    public DateTime BeginWeek { get; set; }

        //    [DataType(DataType.Date)]
        //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //    public DateTime EindeWeek { get; set; }

        //    public List<CreateMenuItemViewModel> MenuItemListViewModels { get; set; }

        //    public MenuViewModel()
        //    {
        //        MenuItemListViewModels = new List<CreateMenuItemViewModel>();
        //    }
        //}

        //public class CreateMenuItemViewModel
        //{
        //    public string Dag { get; set; }

        //    [Required]
        //    [Display(Name = "Voorgerecht")]
        //    public string Voorgerecht { get; set; }

        //    [Required]
        //    [Display(Name = "Hoofdgerecht")]
        //    public string Hoofdgerecht { get; set; }

        //    [Required]
        //    [Display(Name = "Dessert")]
        //    public string Dessert { get; set; }
        //}

        public class MenuViewModel
        {
            public int Id { get; set; }
            public int Week { get; set; }

            public string TypeGebruiker { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase MenuImageUpload { get; set; }

            public string Boodschap { get; set; }

            [Display(Name = "Begin van de week")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime BeginWeek { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime EindeWeek { get; set; }

            public MenuViewModel() { }

            public MenuViewModel(string boodschap, string typeGebruiker)
            {
                Boodschap = boodschap;
                TypeGebruiker = typeGebruiker;
            }

            public MenuViewModel(int id, int week, DateTime beginweek, DateTime eindeweek)
            {
                Id = id;
                Week = week;
                BeginWeek = beginweek;
                EindeWeek = eindeweek;
            }

            public CreateMenuItemMaandagViewModel MaandagViewModel { get; set; }
            public CreateMenuItemDinsdagViewModel DinsdagViewModel { get; set; }
            public CreateMenuItemWoensdagViewModel WoensdagViewModel { get; set; }
            public CreateMenuItemDonderdagViewModel DonderdagViewModel { get; set; }
            public CreateMenuItemVrijdagViewModel VrijdagViewModel { get; set; }
            public CreateMenuItemZaterdagViewModel ZaterdagViewModel { get; set; }
            public CreateMenuItemZondagViewModel ZondagViewModel { get; set; }

        }

        public class CreateMenuItemMaandagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemDinsdagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemWoensdagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemDonderdagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemVrijdagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemZaterdagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }

        public class CreateMenuItemZondagViewModel
        {
            public string Dag { get; set; }

            private string _voorgerecht;
            [Display(Name = "Voorgerecht")]
            public string Voorgerecht
            {
                get { return _voorgerecht; }
                set
                {
                    _voorgerecht = value ?? "Nog niets";
                }
            }

            private string _hoofdgerecht;
            [Display(Name = "Hoofdgerecht")]
            public string Hoofdgerecht
            {
                get { return _hoofdgerecht; }
                set
                {
                    _hoofdgerecht = value ?? "Nog niets";
                }
            }

            private string _dessert;
            [Display(Name = "Dessert")]
            public string Dessert
            {
                get { return _dessert; }
                set
                {
                    _dessert = value ?? "Nog niets";
                }
            }
        }
        #endregion

        //Klachten
        public class ListKlachtViewModel
        {
            public List<KlachtViewModel> List { get; set; }

            public ListKlachtViewModel()
            {
                List = new List<KlachtViewModel>();
            }

            public void AddKlacht(KlachtViewModel klacht)
            {
                List.Add(klacht);
            }
        }

        public class KlachtViewModel
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Omschrijving")]
            public string Omschrijving { get; set; }

            [Display(Name = "Opgegeven door")]
            public string Client { get; set; }

            [Display(Name = "Doorgestuurd op")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
            public DateTime TimeStamp { get; set; }

            public KlachtViewModel() { }

            public KlachtViewModel(int id, string omschrijving, string client, DateTime timestamp)
            {
                Id = id;
                Omschrijving = omschrijving;
                Client = client;
                TimeStamp = timestamp;
            }
        }

        public class KamerOpdrachtListViewModel
        {
            public List<KamerOprachtViewModel> KamerOprachten { get; set; }
            public KamerOprachtViewModel KamerOpracht { get; set; }

            public void AddItem(KamerOprachtViewModel item)
            {
                KamerOprachten.Add(item);
            }

            public KamerOpdrachtListViewModel()
            {
                KamerOprachten = new List<KamerOprachtViewModel>();
            }
        }

        public class KamerOprachtViewModel
        {
            public int Id { get; set; }

            [Required]
            public string Titel { get; set; }

            [Required(ErrorMessage = "Gelieve een foto te selecteren.")]
            [DataType(DataType.Upload)]
            [Display(Name = "Kies een foto")]
            public HttpPostedFileBase ImageUpload { get; set; }

            public string ImageUrl { get; set; }

            public KamerOprachtViewModel() { }

            public KamerOprachtViewModel(int id, string titel, string imageurl)
            {
                Id = id;
                Titel = titel;
                ImageUrl = imageurl;
            }
        }
    }
}