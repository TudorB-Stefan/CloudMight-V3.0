using System.Security.Claims;
using CloudMight.API.Data;
using CloudMight.API.DTOs;
using CloudMight.API.Entities;
using CloudMight.API.Extensions;
using CloudMight.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager,SignInManager<User> signInManager,ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto)
    {
        var partition = new Partition
        {
            Id = Guid.NewGuid().ToString(),
            SizeBytes = 1L *1024*1024*1024,
            UsedBytes = 0,
            MountPath = "mountPath",
            DevicePath = "devicePath"
        };
        var user = new User
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PartitionId = partition.Id,
            Partition = partition
        };
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }

        await userManager.AddToRoleAsync(user, "Member");
        return await user.ToDto(tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized("Invalid email or password");
        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)  return Unauthorized("Invalid email or password");
        return await user.ToDto(tokenService);
    }
}