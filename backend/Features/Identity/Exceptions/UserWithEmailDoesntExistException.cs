using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class UserWithEmailDoesntExistException : BaseException
{
    public UserWithEmailDoesntExistException() : base("Użytkownik o podanym adresie email nie istnieje") { }
}