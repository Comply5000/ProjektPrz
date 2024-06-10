using API.Common.Exceptions;

namespace API.Features.Identity.Exceptions
{
    public class AddUserToRoleExeption:BaseException
    {
        public AddUserToRoleExeption() : base("Błąd dodania roli do uzytkownika")
        {
        }
    }
}
