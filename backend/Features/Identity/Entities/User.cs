using API.Features.Comments.Entities;
using API.Features.Companies.Entities;
using API.Features.Questions.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public Company Company { get; set; }
    
    public List<Company> FavouriteCompanies { get; set; }
    
    public List<Comment> Comments { get; set; }
    public List<Question> Questions { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.FavouriteCompanies)
            .WithMany(x => x.Followers);
        
        builder
            .HasOne(x => x.Company)
            .WithOne(x => x.User)
            .HasForeignKey<Company>(x => x.UserId);
    }
}