using API.Common.Entities;
using API.Common.Enums;
using API.Features.Identity.Entities;
using API.Features.Images.Entities;
using API.Features.Offers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Companies.Entities;

public class Company : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Localization { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid? ImageId { get; set; }
    public Image Image { get; set; }
    
    public List<Offer> Offers { get;set; }
    public List<User> Followers { get; set; }
}

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasQueryFilter(x => x.EntryStatus != EntryStatus.Deleted);
    }
}

