using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IoF_Admin.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IoF_Admin.Controllers
{
    public class ConfigurationController : Controller
    {
        [FromServices]
        public IConfigurationService ConfigurationService { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ConfigurationService.GetConfigurations();
            return View();
        }
    }
}
