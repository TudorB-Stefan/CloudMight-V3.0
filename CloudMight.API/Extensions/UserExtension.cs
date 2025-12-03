using CloudMight.API.DTOs;
using CloudMight.API.Entities;
using CloudMight.API.Interfaces;

namespace CloudMight.API.Extensions;

public static class UserExtension
{
    public static async Task<UserDto> ToDto(this User user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = await tokenService.CreateToken(user),
            Partition = user.Partition == null ? null : new PartitionDto
            {
                Id = user.Partition.Id,
                SizeBytes =user.Partition.SizeBytes,
                UsedBytes = user.Partition.UsedBytes,
                MountPath = user.Partition.MountPath,
                DevicePath = user.Partition.DevicePath
            }
        };
    }
}