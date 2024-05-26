namespace API.Features.Companies.Models;

public class CompanyListModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Localization { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool Favourite { get; set; }
}