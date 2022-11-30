using static HouseRentingSystem.Areas.Admin.Constants.AdminConstant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    [Authorize(Roles = AdminRoleName)]    

    public class BaseController : Controller
    {
       
    }
}
