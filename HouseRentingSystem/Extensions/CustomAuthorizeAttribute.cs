using Microsoft.AspNetCore.Mvc.Filters;

namespace HouseRentingSystem.Extensions
{
	public class CustomAuthorizeAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var user = context.HttpContext.User;

			base.OnActionExecuting(context);
		}
	}
}
