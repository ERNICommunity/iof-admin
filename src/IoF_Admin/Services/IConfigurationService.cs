using IoF_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Services
{
    public interface IConfigurationService
    {
        List<Aquarium> GetConfigurations();
        Aquarium GetConfiguration(int ID);
        bool SetConfiguration(Aquarium aquarium);
        bool DeleteConfiguration(Aquarium aquarium);
    }
}
