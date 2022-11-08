using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agents;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
			//return RedirectToAction(nameof(HousesController.All));
			return RedirectToAction("All", "House");

		}
	}
}
