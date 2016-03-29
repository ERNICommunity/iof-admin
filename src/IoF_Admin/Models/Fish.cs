using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class Fish
    {
        public int ID { get; set; }
        public int Channel { get; set; }
        public int SecondsActive { get; set; }
        public Aquarium Aquarium { get; set; }
    }
}
    