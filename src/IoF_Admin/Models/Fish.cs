using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class Fish
    {
        public int FishID { get; set; }

        [Required]
        [Display(Name = "Channel")]
        public int Channel { get; set; }

        [Display(Name = "Activation Time")]
        public int SecondsActive { get; set; }

        public Aquarium Aquarium { get; set; }
        public int AquariumId { get; set; }

        public Office Office { get;  set;}
        public int OfficeId { get; set; }
    }
}
    