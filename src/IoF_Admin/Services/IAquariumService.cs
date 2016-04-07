using IoF_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Services
{
    public interface IAquariumService
    {
        List<Aquarium> GetAquariums();
        Aquarium GetAquarium(string aquariumMac);
    }
}
