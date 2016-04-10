using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class Office
    {
        public int OfficeID { get; set; }

        [Display(Name = "Office Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Office City")]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }


        #region NotMapped Properties
        /*
         * Unmapped properties are used to display dropdowns for ForeignKeys
         * They are not mapped to database fields and not persisted (unmapped)
         * */
        [NotMapped]
        [Display(Name = "Office")]
        public string OfficeString
        {
            get {
                if (!string.IsNullOrEmpty(this.Name))
                {
                    return string.Format("{0}, {1} ({2})", this.Name, this.City, this.CountryCode);
                }
                return string.Format("{0} ({1})", this.City, this.CountryCode);
            }
            private set { }
        }


        #endregion
    }
}
