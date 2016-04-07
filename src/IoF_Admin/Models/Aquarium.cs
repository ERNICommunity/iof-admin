using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace IoF_Admin.Models
{
    public class Aquarium
    {
        [ScaffoldColumn(false)]
        public string AquariumID { get; set; }

        [Display(Name = "Aquarium Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Hardware ID")]
        public string HardwareID { get; set; }

        [Display(Name = "Aquarium Active")]
        public bool IsActive { get; set; }

        public Office Office { get; set; }
        public List<Fish> Fishes { get; set; }

    }
}
