﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> IsExistsByIdAsync(string userId);

        Task<bool> UserWithThisPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasRents(string userId);

        Task Create(string userId, string phoneNumber);
    }
}
