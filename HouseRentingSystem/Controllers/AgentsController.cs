using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agents;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	[Authorize]

	public class AgentsController : Controller
	{
		private readonly IAgentService agentService;

		public AgentsController(IAgentService _agentService)
		{
			agentService = _agentService;
		}

		public async Task<IActionResult> Become()
		{
			var userId = ClaimsPrincipalExtensions.Id;

            if (await agentService.IsExistsByIdAsync(this.User.Id()))
			{
				return BadRequest();
			}

			var model = new BecomeAgentFormModel();

			return View(model);
		}

		[HttpPost]

		public async Task<IActionResult> Become (BecomeAgentFormModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
            var userId = this.User.Id();

			if (await agentService.IsExistsByIdAsync(userId))
            {
                return BadRequest();
            }

			if (await agentService.UserWithThisPhoneNumberExistsAsync(model.PhoneNumber))
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exists. Enter another one.");
			}

			if (await agentService.UserHasRentsAsync(userId))
			{
				ModelState.AddModelError("Error", "You should have no rents to become and agent!");
			}

			await agentService.CreateAsync(userId, model.PhoneNumber);

            //return RedirectToAction(nameof(HousesController.All));
            return RedirectToAction("All", "Houses");

		}
	}
}
