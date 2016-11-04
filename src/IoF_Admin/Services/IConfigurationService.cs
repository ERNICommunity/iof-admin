using IoF_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets all active configurations
        /// </summary>
        /// <returns>List of active aquariums</returns>
        List<Aquarium> GetConfigurations();

        /// <summary>
        /// Gets a single configuration
        /// </summary>
        /// <param name="aquariumMac">The hardware id (Mac) of an aquarium</param>
        /// <returns>Configuration of Aquarium or new configuration for newly added one.</returns>
        Aquarium GetConfiguration(string aquariumMac);

        /// <summary>
        /// Publishes the configuration to the aquarium using MQTT
        /// </summary>
        /// <param name="aquariumID">The id of an aquarium</param>
        /// <returns><c>true</c> for successfull published, otherwise <c>false</c></returns>
        bool PublishConfiguration(int aquariumID);

        /// <summary>
        /// Publishes the configuration to the aquarium using MQTT
        /// </summary>
        /// <param name="aquariumMac">The hardware id (Mac) of an aquarium</param>
        /// <returns><c>true</c> for successfull published, otherwise <c>false</c></returns>
        bool PublishConfiguration(string aquariumMac);

        /// <summary>
        /// Deletes a configuration
        /// </summary>
        /// <param name="aquariumMac">The hardware id (Mac) of an aquarium</param>
        /// <returns><c>true</c> for successfull deletion, otherwise <c>false</c></returns>
        bool DeleteConfiguration(string aquariumMac);

    }
}
