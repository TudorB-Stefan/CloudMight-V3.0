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

    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpPost("edit-role")]
    public async Task<ActionResult> EditRoles(string userId, [FromQuery] string roles)
    {
        if (string.IsNullOrEmpty(roles)) return BadRequest("You must select a role");
        var selectedRoles = roles.Split(",").ToArray();
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return BadRequest("Could not retrive user");
        var userRoles = await userManager.GetRolesAsync(user);
        var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded) return BadRequest("Error in adding roles");
        result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        return Ok(await userManager.GetRolesAsync(user));
    }
}