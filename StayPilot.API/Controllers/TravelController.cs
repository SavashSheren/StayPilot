using Microsoft.AspNetCore.Mvc;

namespace StayPilot.API.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
