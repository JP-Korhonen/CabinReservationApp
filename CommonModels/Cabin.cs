using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModels
{
    public class Cabin
    {
        public int CabinId { get; set; }
        public int ResortId { get; set; }
        public int PersonId { get; set; }
        [Required]
        [DisplayName("Postinumero")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Majoituspaikan nimi")]
        public string CabinName { get; set; }
        [Required]
        [DisplayName("Osoite")]
        public string Address { get; set; }
        [DisplayName("Hinta € / vrk")]
        public decimal CabinPricePerDay { get; set; }
        [DisplayName("Kuvaus")]
        public string Description { get; set; }
        [DisplayName("Neliöt")]
        public decimal Area { get; set; }
        [DisplayName("Makuuhuoneet")]
        public int Rooms { get; set; }
        public Resort Resort { get; set; }
        public Person Person { get; set; }
        public Post Post { get; set; }
        [JsonIgnore]
        public List<CabinReservation> CabinReservations { get; set; }
        public List<CabinImage> CabinImages { get; set; }
        [NotMapped]
        public decimal ReservationPercentange { get; set; }
    }
}
