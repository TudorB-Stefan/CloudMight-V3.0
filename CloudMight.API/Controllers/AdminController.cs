using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudMight.API.Controllers;

public class AdminController : ControllerBase
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpGet("users-with-roles")]
    public ActionResult GetUsersWithRoles()
    {
        return Ok("Only for admins");
    }
}