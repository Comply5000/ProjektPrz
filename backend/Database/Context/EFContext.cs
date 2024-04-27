using API.Common.Entities;
using API.Common.Enums;
using API.Features.Comments.Entities;
using API.Features.Companies.Entities;
using API.Features.Identity.Entities;
using API.Features.Images.Entities;
using API.Features.Offers.Entities;
using API.Features.Questions.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Database.Context;

public class EFContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Image> Images { get; set; }
    
    public EFContext(DbContextOptions<EFContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is Entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["EntryStatus"] = EntryStatus.Active;
                        break;
                    
                    case EntityState.Deleted:
                        entry.CurrentValues["EntryStatus"] = EntryStatus.Deleted;
                        entry.State = EntityState.Modified;
                        break;
                }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
