using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
