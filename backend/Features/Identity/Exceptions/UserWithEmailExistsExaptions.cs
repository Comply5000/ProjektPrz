using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions
{
    public class UserWithEmailExistsExaptions : BaseException
    {
        public UserWithEmailExistsExaptions() : base("Użytkownik istnieje")
        {
        }
    }
}
