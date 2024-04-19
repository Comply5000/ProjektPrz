using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class UserPasswordExeption : BaseException
    {
        public UserPasswordExeption() : base("wrong password")
        {
        }
    }
}
