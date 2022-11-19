using HouseRentingSystem.Core.Models.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Models.Houses
{
	public class HouseDetailsViewModel : HouseServiceModel
	{
		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;

		public AgentServiceModel Agent { get; set; }
	}
}
