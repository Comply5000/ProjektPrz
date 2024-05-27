using API.Features.Offers.Enums;

namespace API.Features.Offers.Models;

public class OfferModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public OfferType Type { get; set; }
    public string ImageUrl { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    public bool IsUserCommented { get; set; }
    public double? Rating { get; set; }
}