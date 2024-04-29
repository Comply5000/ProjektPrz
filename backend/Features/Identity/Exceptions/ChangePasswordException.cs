using API.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Exceptions;

public sealed class ChangePasswordException : BaseException
{
    public ChangePasswordException() : base("One or more erros occurred during creating user.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ChangePasswordException(IEnumerable<IdentityError> errors) : this()
    {
        var allErrors = errors.GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(a => a.Key, a => a.ToArray());

        if (allErrors.ContainsKey("InvalidToken"))
            Errors.Add("TokenError", new[] { "An error occurred while resetting your password." });
        else
            Errors = allErrors;
    }

    public IDictionary<string, string[]> Errors { get; }
}