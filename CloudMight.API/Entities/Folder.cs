namespace CloudMight.API.Entities;

public class Folder
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public string? PartitionId { get; set; }
    public Partition? Partition { get; set; }
    
    public string? ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }
    
    public List<Folder>? Folders { get; set; }
    public List<File>? Files { get; set; }
}