using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult Patient()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult AdminClinic()
        {
            return View();
        }

        public IActionResult Doctors()
        {
            return View();
        }
    }
}
