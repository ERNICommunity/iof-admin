using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class Fish
    {
        [ScaffoldColumn(false)]
        public int FishID { get; set; }

        [Required]
        [Display(Name = "Channel")]
        public int Channel { get; set; }

        [Display(Name = "Activation Time")]
        public int SecondsActive { get; set; }

        [Required]
        public Aquarium Aquarium { get; set; }

        [Required]
        public Office Office { get;  set;}
    }
}
    