using CloudMight.API.Entities;

namespace CloudMight.API.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}