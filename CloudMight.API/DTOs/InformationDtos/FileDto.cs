using CloudMight.API.Entities;

namespace CloudMight.API.DTOs;

public class FileDto
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string FolderId { get; set; }
    public Folder Folder { get; set; }
}