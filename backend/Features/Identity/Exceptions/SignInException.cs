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

    private const string LockOutError = "Twoje konto zostało zablokowane";
    private const string NotAllowedError = "Nie można zalogować się na nieautoryzowane konto";
    private const string InvalidCredentialsError = "Nieprawidłowe dane";
}