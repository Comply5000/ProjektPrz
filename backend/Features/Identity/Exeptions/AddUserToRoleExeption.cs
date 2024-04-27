using API.Common.Exceptions;

namespace API.Features.Identity.Exeptions
{
    public class AddUserToRoleExeption:BaseException
    {
        public AddUserToRoleExeption() : base("błąd dodania roli do uzytkownika")
        {
        }
    }
}
