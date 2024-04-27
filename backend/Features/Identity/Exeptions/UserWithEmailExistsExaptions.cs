using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class UserWithEmailExistsExaptions : BaseException
    {
        public UserWithEmailExistsExaptions() : base("user istnieje")
        {
        }
    }
}
