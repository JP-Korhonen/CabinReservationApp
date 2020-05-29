using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommonModels
{
    public class ActivityReservation
    {
        public int ActivityReservationId { get; set; }
        public int CabinReservationId { get; set; }
        public int ActivityId { get; set; }
        [Display(Name = "Ajankohta")]
        public DateTime ActivityReservationTime { get; set; }
        [JsonIgnore]
        public CabinReservation CabinReservation { get; set; }
        public Activity Activity { get; set; }
    }
}
