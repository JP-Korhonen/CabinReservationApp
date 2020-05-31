using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class BlobFiles
    {
        [Display(Name = "Tiedoston nimi")]
        public string FileName { get; set; }
        [Display(Name = "Koko")]
        public string FileSize { get; set; }
        [Display(Name = "Muokattu viimeksi")]
        public string ModifiedOn { get; set; }
    }
}
