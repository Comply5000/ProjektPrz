using API.Features.Companies.Entities;
using API.Features.Images.Entities;
using API.Features.Offers.Enums;

namespace API.Features.Offers.Models
{
    public class OffersListModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OfferType Type { get; set; }
        public Image ImageUrl { get; set; }
        public Guid CompanyId { get; set; }
    }
}
