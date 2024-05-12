namespace API.Features.Identity.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid CompanyId { get; }
}