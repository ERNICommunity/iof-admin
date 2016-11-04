using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin
{
    public class AppSettings
    {
        public string MQTTBroker { get; set; }

        public string MQTTPrefix { get; set; }

        public bool UseFackeServices { get; set; }
    }
}
