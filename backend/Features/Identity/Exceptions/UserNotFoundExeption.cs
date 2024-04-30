using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions
{
    public class UserNotFoundExeption : BaseException
    {
        public UserNotFoundExeption():base("user not found")
        {
        }
    }
}
