using API.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Exeptions;

public sealed class SignInException : BaseException
{

    public SignInException(SignInResult result) : base(GetMessageForSignInResult(result)) {}


    private static string GetMessageForSignInResult(SignInResult result)
    {
        if (result == SignInResult.LockedOut)
            return LockOutError;
        if (result == SignInResult.NotAllowed)
            return NotAllowedError;

        return InvalidCredentialsError;

    }

    private const string LockOutError = "Your account has been blocked.";
    private const string NotAllowedError = "Unable to log in to unauthorized account.";
    private const string InvalidCredentialsError = "Invalid credentials.";
}