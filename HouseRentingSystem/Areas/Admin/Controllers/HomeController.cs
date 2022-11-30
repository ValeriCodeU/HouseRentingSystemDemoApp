using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {       
        public IActionResult Index()
        {
            return View();
        }        
    }
}
