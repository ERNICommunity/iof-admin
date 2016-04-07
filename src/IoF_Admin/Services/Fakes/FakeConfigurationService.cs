using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoF_Admin.Models;

namespace IoF_Admin.Services.Fakes
{
    public class FakeConfigurationService : IConfigurationService
    {
        private Office office = new Office { OfficeID = 1, Name = "Bern", City = "Bern", ContactPerson = "Didi", CountryCode = "CH" };
        private Office office2 = new Office { OfficeID = 2, Name = "Zurich", City = "Zurich", ContactPerson = "Jazz", CountryCode = "CH" };

        private List<Fish> fishes = new List<Fish> {
                new Fish { FishID = 1, Channel = 1, SecondsActive = 10 },
                new Fish { FishID = 2, Channel = 4, SecondsActive = 10 },
                new Fish { FishID = 3 }
            };
        
        public Aquarium GetConfiguration(string aquariumMac)
        {
            if(fishes.Count > 2)
            {
                fishes[0].Office = office;
                fishes[1].Office = office2;
            }
            return new Aquarium { AquariumID = 1, IsActive = true, Name = "Test Aquarium", HardwareID = "FF:FF:FF:FF:FF:01", Fishes = fishes, Office = office };
        }

        public List<Aquarium> GetConfigurations()
        {
            var configuredAquarium = GetConfiguration("FF:FF:FF:FF:FF:00");
            return new List<Aquarium>{
                                        configuredAquarium,
                                        new Aquarium { AquariumID= 1234, HardwareID = "FF:FF:FF:FF:FF:02" }
                                     };
        }

        public bool SetConfiguration(Aquarium aquarium)
        {
            return true;
        }

        public bool DeleteConfiguration(Aquarium aquarium)
        {
            return true;
        }
    }
}
