using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Data.DataConstants.Category;

namespace HouseRentingSystem.Infrastructure.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCategoryName)]

        public string Name { get; set; } = null!;

        public List<House> Houses { get; set; } = new List<House>();
    }
}
