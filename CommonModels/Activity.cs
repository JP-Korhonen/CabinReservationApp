using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModels
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int ResortId { get; set; }
        [DisplayName("Postinumero")]
        public string PostalCode { get; set; }
        [DisplayName("Palveluntarjoaja")]
        public string ActivityProvider { get; set; }
        [DisplayName("Lisäpalvelu")]
        public string ActivityName { get; set; }
        [DisplayName("Osoite")]
        public string Address { get; set; }
        [DisplayName("Hinta")]
        public decimal ActivityPrice { get; set; }
        [DisplayName("Kuvaus")]
        public string Description { get; set; }
        public Resort Resort { get; set; }
        public Post Post { get; set; }
        [JsonIgnore]
        public List<ActivityReservation> ActivityReservations { get; set; }
        [NotMapped]
        public int ReservationCount { get; set; }
    }
}
