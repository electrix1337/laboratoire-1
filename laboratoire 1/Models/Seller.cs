using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCRUD_WebApp_JSON.Models
{

    public class Seller
    {
        public int Id { get; set; }

        [Display(Name = "name")]
        [Required(ErrorMessage = "Obligatoire")]
        public string Name { get; set; }

        [Display(Name = "Courriel")]
        [Required(ErrorMessage = "Obligatoire")]
        //[System.Web.Mvc.Remote("EmailAvailable", "Sellers", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Ce courriel n'est pas disponible.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Display(Name = "Téléphone")]
        public string Phone { get; set; }


    }
    public class Sellers
    {
        private static readonly Sellers instance = new Sellers();
        public static Sellers Instance
        {
            get { return instance; }
        }
        private static readonly List<Seller> _sellers = new List<Seller>();

        public static int Count => _sellers.Count;
        public static void LoadColors()
        {
            var httpServerUtility = new HttpServerUtilityWrapper(HttpContext.Current.Server);
            string filePath = httpServerUtility.MapPath("~/App_Data/Sellers.json");
            StreamReader sr = new StreamReader(filePath);
            int colorIndex = 0;
            while (!sr.EndOfStream)
            {
                _sellers.Add(new Seller { Id = colorIndex, Name = sr.ReadLine() });
                colorIndex++;
            }
        }
        public static SelectList ToSelectList(string caption, int selectedId = -1)
        {
            if (_sellers.Count == 0)
                LoadColors();
            if (_sellers.Where(c => c.Id == -1).FirstOrDefault() == null)
                _sellers.Insert(0, new Seller { Id = -1, Name = caption });
            return new SelectList(_sellers,          // colors list
                                    "Id",           // value property
                                    "Name",         // text property
                                    selectedId,     // selected value
                                    new[] { -1 }    // disabled value
                                  );
        }
        public static string Get(int id)
        {
            if (_sellers.Count == 0)
                LoadColors();
            Seller color = _sellers.Where(c => c.Id == id).FirstOrDefault();
            if (color != null)
                return color.Name;
            return "";
        }
    }
}