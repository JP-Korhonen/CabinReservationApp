using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModels
{
    public class Person
    {
        public int PersonId { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Henkilötunnus")]
        public string SocialSecurityNumber { get; set; }
        [Required]
        [Display(Name = "Etunimi")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Sukunimi")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Puhelinnumero")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Osoite")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }
        public Post Post { get; set; }
        [JsonIgnore]
        public List<CabinReservation> CabinReservations { get; set; }
        [JsonIgnore]
        public List<Cabin> Cabins { get; set; }
        [NotMapped]
        [Display(Name = "Rooli")]
        public string Role { get; set; }
    }
}
