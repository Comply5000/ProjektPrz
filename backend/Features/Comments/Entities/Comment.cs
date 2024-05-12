using API.Common.Entities;
using API.Common.Enums;
using API.Features.Identity.Entities;
using API.Features.Offers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Comments.Entities;

public class Comment : Entity
{
    public string Message { get; set; }
    public int Rating { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
}

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasQueryFilter(x => x.EntryStatus != EntryStatus.Deleted);
    }
}
