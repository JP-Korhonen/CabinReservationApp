using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CabinSearch
    {
        [Display(Name = "Saapuminen")]
        public DateTime Arrival { get; set; }
        [Display(Name = "Lähtö")]
        public DateTime Departure { get; set; }
        [Display(Name = "Hakusana")]
        public string SearchWord { get; set; }
        [Display(Name = "Henkilöä")]
        public string Rooms { get; set; }
        [Display(Name = "Järjestä")]
        public string Sort { get; set; }
    }
}
