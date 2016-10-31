using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.ResourceModels
{
    public class ConfigurationResourceModel
    {
        /// <summary>
        /// Mac address of the controller
        /// </summary>
        /// <value>Mac address of the controller</value>
        public string AquariumId { get; set; }

        /// <summary>
        /// Gets or Sets FishMapping
        /// </summary>
        public List<FishMappingResourceModel> Fish { get; set; }

        /// <summary>
        /// Gets or Sets Office
        /// </summary>
        public OfficeResourceModel Office { get; set; }
    }

    public class FishMappingResourceModel
    {
        public string FishId { get; set; }
        public OfficeResourceModel Office { get; set; }
    }

    public class OfficeResourceModel
    {
        public string OfficeId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
