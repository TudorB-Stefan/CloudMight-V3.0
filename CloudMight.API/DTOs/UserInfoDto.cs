namespace CloudMight.API.DTOs;

public class UserInfoDto
{
    public string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public PartitionDto? PartitionDto { get; set; }
}