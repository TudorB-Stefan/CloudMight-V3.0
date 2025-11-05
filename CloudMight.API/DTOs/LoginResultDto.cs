namespace CloudMight.API.DTOs;

public class LoginResultDto
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
}