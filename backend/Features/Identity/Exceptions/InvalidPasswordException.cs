using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class InvalidPasswordException : BaseException
{
    public InvalidPasswordException() : base("Nieprawidłowe hasło")
    {
    }
}