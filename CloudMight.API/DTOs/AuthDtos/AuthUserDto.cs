namespace CloudMight.API.DTOs;

public class AuthUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required List<AuthPartitionDto> Partitions { get; set; }
}