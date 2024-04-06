
namespace API.Features.Identity.Commands.SingUp
{
    internal interface IIdentityService
    {
        Task SingUpCommand(string userName, string email, string password, CancellationToken cancellationToken);
    }
}