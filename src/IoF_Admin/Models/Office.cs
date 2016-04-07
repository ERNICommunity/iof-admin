using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class Office
    {
        [ScaffoldColumn(false)]
        public int OfficeID { get; set; }

        [Display(Name = "Office Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Office City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
        
    }
}
