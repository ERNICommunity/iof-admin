using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using IoF_Admin.Services;
using IoF_Admin.ResourceModels;

namespace IoF_Admin.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _configService;
        private readonly ILogger<ConfigurationController> _log;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationService service, ILogger<ConfigurationController> logger, IMapper mapper)
        {
            _configService = service;
            _log = logger;
            _mapper = mapper;
        }

        // GET: api/configuration
        [HttpGet]
        public IEnumerable<ConfigurationResourceModel> Get()
        {
            var aquariums = _configService.GetConfigurations();
            List<ConfigurationResourceModel> resourceModel = _mapper.Map<List<ConfigurationResourceModel>>(aquariums);
            return resourceModel;
        }

        // GET api/configuration/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var aquarium = _configService.GetConfiguration(id);
            ConfigurationResourceModel resourceModel = _mapper.Map<ConfigurationResourceModel>(aquarium);
            return Json(resourceModel);
        }

        // POST api/configuration
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/configuration/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/configuration/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
