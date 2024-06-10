using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class ResetPasswordException : BaseException
{
    public ResetPasswordException() : base("Wystąpił błąd podczas resetowania hasła") { }
}