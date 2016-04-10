using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace IoF_Admin.Models
{
    public class Aquarium
    {
        public int AquariumID { get; set; }

        [Display(Name = "Aquarium Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Hardware ID")]
        public string HardwareID { get; set; }

        [Display(Name = "Aquarium Active")]
        public bool IsActive { get; set; }

        public Office Office { get; set; }

        public List<Fish> Fishes { get; set; }

        #region NotMapped Properties
        /*
         * Unmapped properties are used to display dropdowns for ForeignKeys
         * They are not mapped to database fields and not persisted (unmapped)
         * */
        [NotMapped]
        public string AquariumString
        {
            get { return string.Format("{0} - {1}", this.AquariumID, this.Name); }
            private set { }
        }


        #endregion

    }
}
