using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class UserWithEmailDoesntExistException : BaseException
{
    public UserWithEmailDoesntExistException() : base("User with that email doesn't exist.") { }
}