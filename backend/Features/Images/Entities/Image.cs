using API.Common.Entities;

namespace API.Features.Images.Entities;

public class Image : Entity
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long TotalBytes { get; set; }
    public string S3Key { get; set; }
    public string Url { get; set; }
}