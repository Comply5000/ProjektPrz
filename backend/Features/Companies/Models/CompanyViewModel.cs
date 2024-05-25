namespace API.Features.Companies.Models
{
    public class CompanyViewModel
    {
        public string Name { get; set; }
        public string Localization { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
    }
}
