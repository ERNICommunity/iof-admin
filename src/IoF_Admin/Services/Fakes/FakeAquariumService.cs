using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoF_Admin.Models;

namespace IoF_Admin.Services.Fakes
{
    public class FakeAquariumService : IAquariumService
    {
        public Aquarium GetAquarium(string aquariumMac)
        {
            throw new NotImplementedException();
        }

        public List<Aquarium> GetAquariums()
        {
            throw new NotImplementedException();
        }
    }
}
