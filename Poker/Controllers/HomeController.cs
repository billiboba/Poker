using Microsoft.AspNetCore.Mvc;

namespace Poker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Welcome to the Poker Game!";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
