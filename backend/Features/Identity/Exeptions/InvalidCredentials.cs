using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class InvalidCredentials : BaseException
    {
        public InvalidCredentials() : base("nieprawidłowe dane")
        {
        }
    }
}
