using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoF_Admin.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IoF_Admin.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IoFContext context;
        private readonly ILogger<ConfigurationService> log;

        public ConfigurationService(IoFContext db, ILogger<ConfigurationService> logger)
        {
            context = db;
            log = logger;
        }

        public bool DeleteConfiguration(Aquarium aquarium)
        {
            var toDelete = context.Aquariums.Single(a => a.AquariumID.Equals(aquarium.AquariumID));
            if(toDelete != null)
            {
                context.Aquariums.Remove(toDelete);
                log.LogInformation("Deleted aquarium with HardwareID: {0}", aquarium.HardwareID);
                return context.SaveChanges() > 0 ? true : false;
            }

            return false;
        }

        public Aquarium GetConfiguration(string aquariumMac)
        {
            var aquarium = context.Aquariums
                .Include(a => a.Fishes)
                .Include(a => a.Office)
                .SingleOrDefault(a => a.HardwareID.Equals(aquariumMac));

            var emptyAquarium = new Aquarium() { HardwareID = aquariumMac, IsActive = false };
            if(aquarium != null && !aquarium.IsActive)
            {
                // Given aquarium is not active, we return an empty config
                log.LogDebug("Return empty aquarium because aquarium with HardwareID {0} is not active.", aquariumMac);
                return emptyAquarium;
            }
            
            if (aquarium == null)
            {
                //NO aquarium with given ID was found so we add a new one to the DB
                context.Aquariums.Add(emptyAquarium);
                if (context.SaveChanges() > 0)
                {
                    log.LogInformation("Added new aquarium with HardwareID: {0}", aquariumMac);
                    aquarium = context.Aquariums
                                .Include(a => a.Fishes)
                                .Include(a => a.Office)
                                .SingleOrDefault(a => a.HardwareID.Equals(aquariumMac));
                   
                }
            }

            return aquarium;
        }

        public List<Aquarium> GetConfigurations()
        {
            return context.Aquariums
                .Include(a => a.Fishes)
                .Include(a => a.Office)
                .Where(a => a.IsActive == true)
                .ToList();
        }

        public bool SetConfiguration(Aquarium aquarium)
        {
            if (aquarium != null)
            {
                context.Update(aquarium);
                log.LogDebug("Update aquarium with HardwareID: {0}", aquarium.HardwareID);
                return context.SaveChanges() > 0 ? true : false;
            }

            return false;
        }
    }
}
