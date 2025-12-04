namespace CloudMight.API.Entities;

public class Partition
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string MountPath { get; set; } = string.Empty;
    public string DevicePath { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public long UsedBytes { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public User User { get; set; }
    
    public string MainFolderId { get; set; }
    public Folder MainFolder { get; set; }
}