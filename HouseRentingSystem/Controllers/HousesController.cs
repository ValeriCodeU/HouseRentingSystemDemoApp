using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	[Authorize]

	public class HousesController : Controller
	{
        private readonly IHouseService houseService;

		public HousesController(IHouseService _houseService)
		{
			houseService = _houseService;
		}

        [AllowAnonymous]

		public IActionResult All()
		{
			var model = new AllHousesQueryModel();

            return View(model);
		}

		public IActionResult Mine()
		{
            var model = new AllHousesQueryModel();

            return View(model);
        }

		public IActionResult Details(int id)
		{
			var model = new HouseDetailsViewModel();


            return View(model);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]

        public async Task<IActionResult> Add(HouseFormModel model)
        {
			if (!ModelState.IsValid)
			{
                return View(model);
            }

			return RedirectToAction(nameof(Details), new { id = "1" });
        }

		public async Task<IActionResult> Edit(int id)
		{
			var model = new HouseFormModel();

			return View(model);
		}

		[HttpPost]

		public async Task<IActionResult> Edit(HouseFormModel model, int id)
		{
			return RedirectToAction(nameof(Details), new { id = "1" });
		}

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]

        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]

        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
