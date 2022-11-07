using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext dbContext;

        public AgentService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<bool> IsExistsById(string userId)
        {
            return await dbContext.Agents.AnyAsync(a => a.UserId == userId);
        }
    }
}
