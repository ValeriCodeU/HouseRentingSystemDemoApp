﻿using HouseRentingSystem.Core.Contracts;
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

		public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
		{
			var resultQuery = await houseService.AllAsync(
				query.Category,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				AllHousesQueryModel.HousesPerPage);

			query.TotalHousesCount = resultQuery.TotalHousesCount;
			query.Houses = resultQuery.Houses;

			query.Categories = await houseService.AllGategoriesNamesAsync();			

            return View(query);
		}

		public async Task<IActionResult> Mine()
		{
			IEnumerable<HouseServiceModel> myHouses;

			var userId = User.Id();

			if (await agentService.IsExistsByIdAsync(userId))
			{
				var currengAgentId = await agentService.GetAgentIdAsync(userId);

				myHouses = await houseService.AllHousesByAgentIdAsync(currengAgentId);
			}
			else
			{
				myHouses = await houseService.AllHousesByUserIdAsync(userId);
			}

            return View(myHouses);
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
