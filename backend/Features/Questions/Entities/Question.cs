using API.Common.Entities;
using API.Common.Enums;
using API.Features.Identity.Entities;
using API.Features.Offers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Questions.Entities;

public class Question : Entity
{
    public string Message { get; set; }
    public string Answer { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? AnsweredAt { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
}

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasQueryFilter(x => x.EntryStatus != EntryStatus.Deleted);
    }
}