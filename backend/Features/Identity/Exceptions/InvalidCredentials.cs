using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions
{
    public class InvalidCredentials : BaseException
    {
        public InvalidCredentials() : base("nieprawidłowe dane")
        {
        }
    }
}
