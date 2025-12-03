namespace CloudMight.API.DTOs;

public class PartitionDto
{
    public required string Id { get; set; }
    public required long SizeBytes { get; set; }
    public required long UsedBytes { get; set; }
    public required string MountPath { get; set; }
    public required string DevicePath { get; set; }
}