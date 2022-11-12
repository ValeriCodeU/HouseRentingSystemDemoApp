using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Data.DataConstants.House;

namespace HouseRentingSystem.Core.Models.Houses
{
	public class HouseFormModel
	{
		[Required]
		[StringLength(MaxHouseTitle, MinimumLength = MinHouseTitle)]

		public string Title { get; set; } = null!;

		[Required]
		[StringLength(MaxHouseAddress, MinimumLength = MinHouseAddress)]

		public string Address { get; set; } = null!;

		[Required]
		[StringLength(MaxHouseDescription, MinimumLength = MinHouseDescription)]

		public string Description { get; set; } = null!;

		[Required]
		[Display(Name = "Image URL")]

		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Price Per Month")]
		[Range(typeof(decimal), MinPricePerMonth, MaxPricePerMonth,
			ErrorMessage = "Price Per Month must be a positive number and less than {2} leva.")]

		public decimal PricePerMonth { get; set; }

		[Display(Name = "Category")]

		public int CategoryId { get; set; }

		public IEnumerable<HouseCategoryModel> Categories { get; set; } = new List<HouseCategoryModel>();
	}
}
