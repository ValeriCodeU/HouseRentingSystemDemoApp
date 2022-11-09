using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Data.DataConstants.Agent;

namespace HouseRentingSystem.Core.Models.Agents
{
	public class BecomeAgentFormModel
	{
		[Required]
		[MaxLength(MaxPhoneNumber)]
		[MinLength(MinPhoneNumber)]
		[Display(Name = "Phone Number")]
		[Phone]

		public string PhoneNumber { get; set; } = null!;
	}
}
