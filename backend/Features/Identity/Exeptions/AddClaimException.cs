using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class AddClaimException : BaseException
    {
        public AddClaimException() : base("błąd dodania claimu do uzytkownika")
        {
        }
    }
}
