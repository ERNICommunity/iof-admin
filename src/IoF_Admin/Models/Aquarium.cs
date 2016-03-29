using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IoF_Admin.Models
{
    public class Aquarium
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HardwareID { get; set; }        
        public string IP { get; set; }
        public bool IsActive { get; set; }

        public Office Office { get; set; }
        public List<Fish> Fishes { get; set; }

    }
}
