using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CustomRequired : RequiredAttribute
    {
        public CustomRequired()
        {
            this.ErrorMessage = "Kenttä ei voi olla tyhjä";
        }
    }
}
