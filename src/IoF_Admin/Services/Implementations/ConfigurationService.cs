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

            if(aquarium == null)
            {
                //NO aquarium with given ID was found, add it with empt config to DB
                context.Aquariums.Add(new Aquarium() { HardwareID = aquariumMac, IsActive = false });
                if (context.SaveChanges() > 0)
                {
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
                .ToList();
        }

        public bool SetConfiguration(Aquarium aquarium)
        {
            if (aquarium != null)
            {
                context.Update(aquarium);
                return context.SaveChanges() > 0 ? true : false;
            }

            return false;
        }
    }
}
