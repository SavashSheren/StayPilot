using Microsoft.AspNetCore.Mvc;

namespace StayPilot.API.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
