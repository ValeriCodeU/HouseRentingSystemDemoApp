using HouseRentingSystem.Core.Models.Agents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	[Authorize]

	public class AgentsController : Controller
	{
		public IActionResult Become()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Become (BecomeAgentFormModel model)
		{
			//return RedirectToAction(nameof(HousesController.All));
			return RedirectToAction("All", "House");

		}
	}
}
