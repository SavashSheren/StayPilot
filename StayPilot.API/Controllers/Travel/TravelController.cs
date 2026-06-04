using Microsoft.AspNetCore.Mvc;

namespace StayPilot.API.Controllers.Travel
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
