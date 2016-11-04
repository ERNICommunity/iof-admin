using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using M2Mqtt;
using M2Mqtt.Messages;
using AutoMapper;
using IoF_Admin.Models;
using IoF_Admin.ResourceModels;

namespace IoF_Admin.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IoFContext context;
        private readonly ILogger<ConfigurationService> log;
        private readonly IMapper mapper;
        private readonly AppSettings settings;

        public ConfigurationService(IoFContext db, ILogger<ConfigurationService> logger, IMapper map, IOptions<AppSettings> appSettings)
        {
            context = db;
            log = logger;
            mapper = map;
            settings = appSettings.Value;
        }

        public bool DeleteConfiguration(string aquariumMac)
        {
            var toDelete = context.Aquariums.Single(a => a.HardwareID.Equals(aquariumMac));
            if(toDelete != null)
            {
                context.Aquariums.Remove(toDelete);
                log.LogInformation("Deleted aquarium with HardwareID: {0}", aquariumMac);
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
            publishConfiguration(aquarium.AquariumID);
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

        public bool PublishConfiguration(int aquariumID)
        {
            return publishConfiguration(aquariumID);
        }

        public bool PublishConfiguration(string aquariumMac)
        {
            return publishConfiguration(0, aquariumMac);
        }

        private bool publishConfiguration(int aquariumID = 0, string aquariumMac ="")
        {
            var aquarium = context.Aquariums
                                    .Include(a => a.Fishes)
                                    .Include(a => a.Office)
                                    .SingleOrDefault(a => a.AquariumID == aquariumID || a.HardwareID.Equals(aquariumMac));

            if (aquarium != null)
            {
                ConfigurationResourceModel resourceModel = mapper.Map<ConfigurationResourceModel>(aquarium);
                //iof/config/<Device-ID>
                string path = string.Format("{0}/{1}/{2}", settings.MQTTPrefix, "config", aquarium.HardwareID);
                string payload = JsonConvert.SerializeObject(resourceModel,
                    Formatting.None,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                MqttClient client = new MqttClient(settings.MQTTBroker);
                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                // publish a message topic with QoS 1 and retain false
                client.Publish(path.ToLower(), System.Text.Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
                log.LogDebug("Publish MQTT topic {0} with payload {1} to {2}", path, payload, settings.MQTTBroker);

                log.LogDebug("Update aquarium with HardwareID: {0}", aquarium.HardwareID);
                return true;
            }

            return false;
        }
    }
}
