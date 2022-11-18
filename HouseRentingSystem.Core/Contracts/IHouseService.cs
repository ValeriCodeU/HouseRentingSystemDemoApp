using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Core.Models.Houses.Enums;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
	{
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryModel>> AllCategoriesAsync();

        Task<bool> CategoryExists(int categoryId);

        Task<int> CreateAsync(HouseFormModel model, int agentId);

        Task<HouseQueryServiceModel> AllAsync(
            string? category = null, 
            string? searchTerm = null,          
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<string>> AllGategoriesNamesAsync();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId);
    }
}
