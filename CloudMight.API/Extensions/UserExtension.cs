using CloudMight.API.DTOs;
using CloudMight.API.Entities;
using CloudMight.API.Interfaces;

namespace CloudMight.API.Extensions;

public static class UserExtension
{
    public static async Task<AuthResponseDto> ToDto(this User user, ITokenService tokenService)
    {
        return new AuthResponseDto
        {
            RefreshToken = await tokenService.CreateToken(user),
            RefreshTokenExpiry = DateTime.UtcNow.AddHours(2),
            AuthUserDto = new AuthUserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                Partitions = user.Partitions?.Select(p => new AuthPartitionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SizeBytes = p.SizeBytes,
                    UsedBytes = p.UsedBytes,
                    MainFolderId = p.MainFolderId,
                    
                }).ToList() ?? new List<AuthPartitionDto>()
            }
        };
    }
}