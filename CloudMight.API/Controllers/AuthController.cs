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
public class AuthController(AppDbContext context, UserManager<User> userManager,ITokenService tokenService) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly AppDbContext _context = context;
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody]RegisterDto registerDto)
    {
        var mainFolder = new Folder
        {
            Id = Guid.NewGuid().ToString(),
            Name = "root",
            CreatedAt = DateTime.UtcNow,
        };
        var partition = new Partition
        {
            Id = Guid.NewGuid().ToString(),
            Name = $"{registerDto.UserName}-root-partition",
            MountPath = "mountPath",
            DevicePath = "devicePath",
            SizeBytes = 1L *1024*1024*1024,
            UsedBytes = 0,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            MainFolderId = mainFolder.Id
        };
        partition.MainFolderId = mainFolder.Id;
        partition.MainFolder = mainFolder;
        mainFolder.PartitionId = partition.Id;
        var user = new User
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }

        partition.UserId = user.Id;
        await _context.Partitions.AddAsync(partition);
        await _context.Folders.AddAsync(mainFolder);
        await _context.SaveChangesAsync();
        
        await _userManager.AddToRoleAsync(user, "Member");
        
        return await user.ToDto(_tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized("Invalid email or password");
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)  return Unauthorized("Invalid email or password");
        await _context.Entry(user).Collection(u => u.Partitions).LoadAsync();
        return await user.ToDto(_tokenService);
    }
    
}