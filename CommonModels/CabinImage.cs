using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModels
{
    public class CabinImage
    {
        public int CabinImageId { get; set; }
        public int CabinId { get; set; }
        [Display(Name = "Url-osoite")]
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public Cabin Cabin { get; set; }
        [NotMapped]
        public IFormFile Files { get; set; }
    }
}
