using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class ConfirmAccountException : BaseException
{
    public ConfirmAccountException() : base("Wystąpił błąd podczas potwierdzania konta") { }
}