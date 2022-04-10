namespace CareerApplication.Core.Models;

public class File
{
    public string FullPath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public Stream? FileStream { get; set; }
}
