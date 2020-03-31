using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using istp_laba1.Models;

namespace istp_laba1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Students()
        {
            return RedirectToAction("Index", "Students");
        }

        public IActionResult Abonements()
        {
            return RedirectToAction("Index", "Abonements");
        }
        public IActionResult Types()
        {
            return RedirectToAction("Index", "AbonementTypes");
        }

        public IActionResult Styles()
        {
            return RedirectToAction("Index", "Styles");
        }

        public IActionResult Teachers()
        {
            return RedirectToAction("Index", "Teachers");
        }
        public IActionResult Lessons()
        {
            return RedirectToAction("Index", "Lessons");
        }
        public IActionResult Journal()
        {
            return RedirectToAction("Index", "LessonTeachers");
        }
        public IActionResult Classrooms()
        {
            return RedirectToAction("Index", "Classrooms");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
