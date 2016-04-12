using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace IoF_Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Welcome to the world of Fish.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact the IoF Team if you are interested in this cool project.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
