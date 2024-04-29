using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class ConfirmAccountException : BaseException
{
    public ConfirmAccountException() : base("An error occurred while confirming the account.") { }
}