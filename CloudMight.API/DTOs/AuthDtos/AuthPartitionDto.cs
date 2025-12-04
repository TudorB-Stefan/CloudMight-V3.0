namespace CloudMight.API.DTOs;

public class AuthPartitionDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required long SizeBytes { get; set; }
    public required long UsedBytes { get; set; }
    public required string MainFolderId { get; set; }
}