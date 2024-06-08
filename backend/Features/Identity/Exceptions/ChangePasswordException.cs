using API.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Exceptions;

public sealed class ChangePasswordException : BaseException
{
    public ChangePasswordException() : base("Podczas tworzenia użytkownika wystąpił jeden lub więcej błędów")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ChangePasswordException(IEnumerable<IdentityError> errors) : this()
    {
        var allErrors = errors.GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(a => a.Key, a => a.ToArray());

        if (allErrors.ContainsKey("InvalidToken"))
            Errors.Add("TokenError", new[] { "Wystąpił błąd podczas resetowania hasła" });
        else
            Errors = allErrors;
    }

    public IDictionary<string, string[]> Errors { get; }
}