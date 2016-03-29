using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoF_Admin.Models;

namespace IoF_Admin.Services.Fakes
{
    public class FakeConfigurationService : IConfigurationService
    {
        private Office office = new Office { ID = 1, Name = "Bern", City = "Bern", ContactPerson = "Didi", CountryCode = "CH" };
        private List<Fish> fishes = new List<Fish> {
                new Fish { ID = 1, Channel = 1, SecondsActive = 10 },
                new Fish { ID = 2, Channel = 4, SecondsActive = 10 },
                new Fish { ID = 3 }
            };
        
        public Aquarium GetConfiguration(int ID)
        {
            return new Aquarium { ID = 1, IsActive = true, Name = "Test Aquarium", HardwareID = "FF:FF:FF:FF:FF:01", IP = "192.168.0.10", Fishes = fishes, Office = office };
        }

        public List<Aquarium> GetConfigurations()
        {
            var configuredAquarium = GetConfiguration(1);
            return new List<Aquarium>{
                                        configuredAquarium,
                                        new Aquarium { ID= 1234, HardwareID = "FF:FF:FF:FF:FF:02" }
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
