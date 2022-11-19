using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Core.Models.Houses.Enums;
using HouseRentingSystem.Infrastructure.Data;
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

        public async Task<HouseQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)

        {
            var housesQuery = dbContext.Houses.AsQueryable();

            if (!String.IsNullOrEmpty(category))
            {
                housesQuery = dbContext.Houses.Where(c => c.Category.Name == category);
            }

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                housesQuery = housesQuery.Where(
                    h => EF.Functions.Like(h.Title.ToLower(), searchTerm) ||
                EF.Functions.Like(h.Address.ToLower(), searchTerm) ||
                EF.Functions.Like(h.Description.ToLower(), searchTerm));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.Id),
                HouseSorting.Newest => housesQuery.OrderBy(h => h.PricePerMonth),
                _ => housesQuery.OrderByDescending(h => h.Id)
            };

            var houses = await housesQuery.Skip((currentPage - 1) * housesPerPage).Take(housesPerPage)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth
                }).ToListAsync();

            var totalHouses = await housesQuery.CountAsync();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = houses
            };
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
            var categoryNames = await dbContext.Categories.Select(c => c.Name).Distinct().ToListAsync();

            return categoryNames;
        }

        public Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId)
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
