using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Crud.Controllers
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
            //return View();
            return Ok(new { mensaje = "API funcionando correctamente" });
        }

        public IActionResult Privacy()
        {
            //return View();
            return Ok(new { mensaje = "API funcionando correctamente" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
