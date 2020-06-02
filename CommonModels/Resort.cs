using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModels
{
    public class Resort
    {
        public int ResortId { get; set; }
        [Required(ErrorMessage = "Kenttä ei voi olla tyhjä")]
        [Display(Name = "Toimipiste")]
        public string ResortName { get; set; }
        [JsonIgnore]
        public List<Cabin> Cabins { get; set; }
        [JsonIgnore]
        public List<Activity> Activities { get; set; }
        [NotMapped]
        public decimal CabinsReservationsPercentange { get; set; }
        [NotMapped]
        public int ActivitiesReservationsCount { get; set; }
    }
}
