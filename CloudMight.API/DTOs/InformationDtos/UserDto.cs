namespace CloudMight.API.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public required List<PartitionDto> Partitions { get; set; }
}