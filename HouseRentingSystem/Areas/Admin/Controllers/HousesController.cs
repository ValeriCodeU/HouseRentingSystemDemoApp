using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.MyHouses;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
	public class HousesController : BaseController
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

		public async Task<IActionResult> Mine()
		{
			var myHouses = new MyHousesViewModel();
			var adminUserId = User.Id();
			myHouses.RentedHouses = await houseService.AllHousesByUserIdAsync(adminUserId);

			var adminAgent = await agentService.GetAgentIdAsync(adminUserId);
			myHouses.AddedHouses = await houseService.AllHousesByAgentIdAsync(adminAgent);

			return View(myHouses);
;
		}
	}
}
