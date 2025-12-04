namespace CloudMight.API.DTOs;

public class AuthResponseDto
{
    public required string RefreshToken { get; set; }
    public required DateTime RefreshTokenExpiry { get; set; }
    public required AuthUserDto AuthUserDto { get; set; }
}