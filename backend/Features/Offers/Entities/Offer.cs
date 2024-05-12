using API.Common.Entities;
using API.Common.Enums;
using API.Features.Comments.Entities;
using API.Features.Companies.Entities;
using API.Features.Images.Entities;
using API.Features.Offers.Enums;
using API.Features.Questions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Offers.Entities;

public class Offer : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public OfferType Type { get; set; }
    
    public Guid? ImageId { get; set; }
    public Image Image { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    
    public List<Comment> Comments { get; set; }
    public List<Question> Questions { get; set; } 
    
}

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasQueryFilter(x => x.EntryStatus != EntryStatus.Deleted);
    }
}