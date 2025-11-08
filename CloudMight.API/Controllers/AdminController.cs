using CloudMight.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.API.Controllers;

public class AdminController(UserManager<User> userManager) : ControllerBase
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await userManager.Users.ToListAsync();
        var userList = new List<object>();
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            userList.Add(new
            {
                user.Id,
                user.Email,
                Roles = roles.ToList()
            });
        }
        return Ok(userList);
    }
}