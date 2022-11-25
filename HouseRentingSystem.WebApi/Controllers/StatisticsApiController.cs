﻿using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService _statisticsService)
        {
            statisticsService = _statisticsService; 
        }

        /// <summary>
        /// Gets statistics about number of houses and rented houses
        /// </summary>
        /// <returns>Total houses and total rents</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetStatistics()
        {
            var model = await statisticsService.TotalAsync();

            return Ok(model);
        }
    }
}
