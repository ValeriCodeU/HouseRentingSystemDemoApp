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

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
        {
            return await dbContext
                .Houses
                .Where(h => h.AgentId == agentId)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId)
        {
            return await dbContext
                .Houses
                .Where(h => h.RenterId == userId)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                })
                .ToListAsync();
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

        public async Task EditHouseAsync(int houseId, HouseFormModel model)
        {
            var house = await dbContext.Houses.FindAsync(houseId);

            house.Title = model.Title;
            house.Address = model.Address;
            house.Description = model.Description;
            house.ImageUrl = model.ImageUrl;
            house.PricePerMonth = model.PricePerMonth;
            house.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetHouseCategoryIdAsync(int houseId)
        {
            return (await dbContext.Houses.FindAsync(houseId)).CategoryId;
        }

        public async Task<bool> HasAgentWithIdAsync(int houseId, string currentUserId)
        {
            var house = await dbContext.Houses.FindAsync(houseId);

            var agent = await dbContext.Agents.FirstOrDefaultAsync(a => a.Id == house.AgentId);

            if (agent == null)
            {
                return false;
            }

            if (agent.UserId != currentUserId)
            {
                return false;
            }

            return true;
        }

        public async Task<HouseDetailsViewModel> HouseDetailsByIdAsync(int id)
        {
            return await dbContext.Houses
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsViewModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category.Name,
                    Agent = new Models.Agents.AgentServiceModel()
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email
                    }
                })
                .FirstAsync();
        }

        public async Task<bool> HouseExistsAsync(int id)
        {
            return await dbContext.Houses.AnyAsync(h => h.Id == id);
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
