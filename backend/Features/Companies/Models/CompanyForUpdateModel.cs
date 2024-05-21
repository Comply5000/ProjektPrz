namespace API.Features.Companies.Models;

public sealed class CompanyForUpdateModel
{
    public string Name { get; set; }
    public string Localization { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}