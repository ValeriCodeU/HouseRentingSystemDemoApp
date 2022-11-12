using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	[Authorize]

	public class HousesController : Controller
	{
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

		public HousesController(
			IHouseService _houseService, 
			IAgentService _agentService)
		{
			houseService = _houseService;
			agentService = _agentService;
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

		public async Task<IActionResult> Add()
		{
			if (!await agentService.IsExistsByIdAsync(this.User.Id()))
			{
				return RedirectToAction(nameof(AgentsController.Become), "Agent");
			}

			var model = new HouseFormModel()
			{
				Categories = await houseService.AllCategoriesAsync()
			};

			return View(model);
		}

		[HttpPost]

        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (!await agentService.IsExistsByIdAsync(this.User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agent");
            }


            if (!await houseService.CategoryExists(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
			{
				model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

			int agentId = await agentService.GetAgentIdAsync(this.User.Id());

			int id = await houseService.CreateAsync(model, agentId);

            return RedirectToAction(nameof(Details), new { id });
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
