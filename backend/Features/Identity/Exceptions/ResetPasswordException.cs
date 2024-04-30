using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions;

public sealed class ResetPasswordException : BaseException
{
    public ResetPasswordException() : base("An error occurred while resetting your password.") { }
}