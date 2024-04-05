using API.Common.Entities;
using API.Features.Identity.Entities;
using API.Features.Offers.Entities;

namespace API.Features.Questions.Entities;

public class Question : Entity
{
    public string Message { get; set; }
    public string Answer { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
}