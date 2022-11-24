using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Statistics;
using HouseRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HouseRentingDbContext dbContext;

        public StatisticsService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<StatisticsServiceModel> TotalAsync()
        {
            var totalHouse = await dbContext.Houses.CountAsync();

            var totalRents = await dbContext.Houses.Where(h => h.RenterId != null).CountAsync();

            return new StatisticsServiceModel()
            {
                TotalHouses = totalHouse,
                TotalRents = totalRents
            };
        }
    }
}
