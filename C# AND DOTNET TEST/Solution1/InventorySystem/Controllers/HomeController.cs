using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Privacy() => View();
    }
}
