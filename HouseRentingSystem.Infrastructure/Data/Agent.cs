using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Data.DataConstants.Agent;

namespace HouseRentingSystem.Infrastructure.Data
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumber)]

        public string PhoneNumber { get; set; } = null!;

        [Required]

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]

        public IdentityUser User { get; set; } = null!;
    }
}
