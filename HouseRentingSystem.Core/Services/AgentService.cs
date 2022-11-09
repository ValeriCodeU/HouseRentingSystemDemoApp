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

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistsByIdAsync(string userId)
        {
            return await dbContext.Agents.AnyAsync(a => a.UserId == userId);
        }

        public Task<bool> UserHasRents(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserWithThisPhoneNumberExistsAsync(string phoneNumber)
        {
            return await dbContext.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
