using Microsoft.AspNetCore.Mvc;

namespace StayPilot.API.Controllers
{
    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
