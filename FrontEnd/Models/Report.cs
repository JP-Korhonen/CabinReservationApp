using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class Report
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<SelectListItem> Resorts { get; set; }
    }
}
