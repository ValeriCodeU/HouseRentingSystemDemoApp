using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Infrastructure.Data;
using HouseRentingSystem.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<HouseQueryServiceModel> AllAsync(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HouseCategoryModel>> AllCategoriesAsync()
        {
            return await dbContext.Categories
                .OrderBy(c => c.Name)
                .Select(c => new HouseCategoryModel()                
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllGategoriesNamesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateAsync(HouseFormModel model, int agentId)
        {
            var house = new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.CategoryId,
                AgentId = agentId                
            };

            await dbContext.Houses.AddAsync(house);
            await dbContext.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
        {
            var model = await dbContext.Houses
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexServiceModel()                
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl,
                })
                .Take(3)
                .ToListAsync();

            return model;
        }
    }
}
