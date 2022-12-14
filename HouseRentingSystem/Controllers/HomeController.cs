using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static HouseRentingSystem.Areas.Admin.Constants.AdminConstant;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService _houseService)
        {
            houseService = _houseService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            var model = await houseService.LastThreeHousesAsync();

            return View(model);
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}