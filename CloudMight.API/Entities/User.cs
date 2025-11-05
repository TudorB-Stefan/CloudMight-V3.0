using Microsoft.AspNetCore.Identity;

namespace CloudMight.API.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public string PartitionId { get; set; }
    public Partition Partition { get; set; } = null!;
}