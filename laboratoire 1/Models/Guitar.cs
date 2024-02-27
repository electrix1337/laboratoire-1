using JsonDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCRUD_WebApp_JSON.Models
{
    public enum GuitarType { Classique, Acoustique, Électrique }
    public enum ConditionType { Neuf, Usagé }

    public class Guitar
    {
        const string GuitarImagesFolder = @"/Images_Data/Guitars/";
        const string DefaultGuitarImage = @"defaultGuitarLogo.png";

        public Guitar()
        {
            Image = GuitarImagesFolder + DefaultGuitarImage;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un vendeur")]
        public int SellerId { get; set; }

        [Display(Name = "Vendeur")]
        public Seller Seller { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Fabriquant")]
        public string Maker { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Modèle")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner une image")]
        [Display(Name = "Image")]
        [Asset(GuitarImagesFolder)]
        public string Image { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        public string Description { get; set; }

        [Range(1800, 2024, ErrorMessage = "Le nombre doit être entre 1800 et 2024")]
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Année")]
        public int Year { get; set; } = DateTime.Now.Year;

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AddDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Un choix est requis")]
        [Display(Name = "État")]
        public ConditionType? Condition { get; set; }

        [Required(ErrorMessage = "Un choix est requis")]
        [Display(Name = "Type")]
        public GuitarType? GuitarType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le prix doit être valide")]
        [Display(Name = "Prix")]
        [Required(ErrorMessage = "Le prix est requis")]
        public int Price { get; set; }
    }
}