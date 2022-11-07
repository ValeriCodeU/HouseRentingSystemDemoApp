using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        {
            var model = await context.Houses
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
