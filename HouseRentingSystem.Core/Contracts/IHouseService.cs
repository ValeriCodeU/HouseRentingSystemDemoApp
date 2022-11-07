using HouseRentingSystem.Core.Models.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Contracts
{
	public interface IHouseService
	{
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
    }
}
