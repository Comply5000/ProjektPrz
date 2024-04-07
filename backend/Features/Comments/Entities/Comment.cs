using API.Common.Entities;
using API.Features.Identity.Entities;
using API.Features.Offers.Entities;

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
