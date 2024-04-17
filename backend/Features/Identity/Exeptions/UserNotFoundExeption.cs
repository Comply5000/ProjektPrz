using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class UserNotFoundExeption : BaseException
    {
        public UserNotFoundExeption():base("user not found")
        {
        }
    }
}
