using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Data.DataConstants.House;


namespace HouseRentingSystem.Infrastructure.Data
{
    public class House
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxHouseTitle)]

        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MaxHouseAddress)]

        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(MaxHouseDescription)]

        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(MaxHouseImageUrl)]

        public string ImageUrl { get; set; } = null!;

        [Column(TypeName = "Money")]
        [Precision(PrecisionDecimal, ScaleDecimal)]

        public decimal PricePerMonth { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]

        public Category Category { get; set; } = null!;

        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]

        public Agent Agent { get; set; } = null!;
       
        public string? RenterId { get; set; }

        [ForeignKey(nameof(RenterId))]

        public IdentityUser? Renter { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
