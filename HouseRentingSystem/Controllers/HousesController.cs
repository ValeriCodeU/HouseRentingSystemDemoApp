using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Extensions;
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

		public async Task<IActionResult> Details(int id, string information)
		{
			if (!await houseService.HouseExistsAsync(id))
			{
				return RedirectToAction(nameof(All));
			}			

			var model = await houseService.HouseDetailsByIdAsync(id);

            if (information != model.GetInformation())
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

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

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }

		public async Task<IActionResult> Edit(int id)
		{
			var model = new HouseFormModel();

			if (!await houseService.HouseExistsAsync(id))
			{
				return RedirectToAction(nameof(All));
			}

			if (!await houseService.HasAgentWithIdAsync(id, this.User.Id()))
			{
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			var house = await houseService.HouseDetailsByIdAsync(id);

			var houseCategoryId = await houseService.GetHouseCategoryIdAsync(house.Id);

			model.Title = house.Title;
			model.Description = house.Description;
			model.CategoryId = houseCategoryId;
			model.Categories = await houseService.AllCategoriesAsync();
			model.PricePerMonth = house.PricePerMonth;
			model.ImageUrl = house.ImageUrl;
			model.Address = house.Address;			

			return View(model);
		}

		[HttpPost]

		public async Task<IActionResult> Edit(HouseFormModel model, int id)
		{			
            if (!await houseService.HouseExistsAsync(id))
            {
				ModelState.AddModelError("", "House does not exist!");
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            if (!await houseService.HasAgentWithIdAsync(id, this.User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			if (!await houseService.CategoryExists(model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await houseService.AllCategoriesAsync();
				return View(model);
			}

			await houseService.EditHouseAsync(id, model);

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
		}

		public async Task<IActionResult> Delete(int id)
		{
            if (!await houseService.HouseExistsAsync(id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await houseService.HasAgentWithIdAsync(id, this.User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HouseDetailsByIdAsync(id);

			var model = new HouseDeleteViewModel()
			{

				Title = house.Title,
				Address = house.Address,
				ImageUrl = house.ImageUrl
			};

			return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(HouseDeleteViewModel model)
        {
            if (!await houseService.HouseExistsAsync(model.Id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await houseService.HasAgentWithIdAsync(model.Id, this.User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			await houseService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(All));
        }

        [HttpPost]

        public async Task<IActionResult> Rent(int id)
        {
            if (!await houseService.HouseExistsAsync(id))
            {
                return RedirectToAction(nameof(All));
            }

            if (await agentService.IsExistsByIdAsync(this.User.Id()))
            {
                //return BadRequest();
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			if (await houseService.IsRentedAsync(id))
			{
                return RedirectToAction(nameof(All));
            }

			await houseService.RentAsync(id, this.User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]

        public async Task<IActionResult> Leave(int id)
        {
            if (!await houseService.HouseExistsAsync(id) || !await houseService.IsRentedAsync(id))
            {
                return RedirectToAction(nameof(All));
            }

			if (await houseService.IsRentedByUserWithIdAsync(id, this.User.Id()))
			{
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			await houseService.LeaveAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
