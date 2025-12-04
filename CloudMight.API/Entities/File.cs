namespace CloudMight.API.Entities;

public class File
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public string FolderId { get; set; }
    public Folder Folder { get; set; }
}