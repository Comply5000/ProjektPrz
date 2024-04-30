using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions
{
    public class AddClaimException : BaseException
    {
        public AddClaimException() : base("błąd dodania claimu do uzytkownika")
        {
        }
    }
}
