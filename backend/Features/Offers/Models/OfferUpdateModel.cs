using API.Features.Offers.Enums;

namespace API.Features.Offers.Models;

public sealed class OfferUpdateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public string ImageUrl { get; set; }
    public OfferType Type { get; set; }
}