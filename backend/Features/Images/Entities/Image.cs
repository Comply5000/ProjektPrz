using API.Common.Entities;
using API.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Images.Entities;

public class Image : Entity
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long TotalBytes { get; set; }
    public string S3Key { get; set; }
    public string Url { get; set; }
}

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasQueryFilter(x => x.EntryStatus != EntryStatus.Deleted);
    }
}