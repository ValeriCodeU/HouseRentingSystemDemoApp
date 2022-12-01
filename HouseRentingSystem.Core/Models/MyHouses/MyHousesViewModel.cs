using HouseRentingSystem.Core.Models.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Models.MyHouses
{
	public class MyHousesViewModel
	{
		public IEnumerable<HouseServiceModel> AddedHouses { get; set; } = new List<HouseServiceModel>();

		public IEnumerable<HouseServiceModel> RentedHouses { get; set; } = new List<HouseServiceModel>();
	}
}
