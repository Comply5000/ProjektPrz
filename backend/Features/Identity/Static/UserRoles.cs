using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Static
{
    public static class UserRoles
    {
        public const string User = nameof(User);
        public const string CompanyOwner = nameof(CompanyOwner);
        public const string Admin = nameof(Admin);

        private static List<IdentityRole<Guid>> Roles;

        static UserRoles()
        {
            Roles = new List<IdentityRole<Guid>>()
            {
                new(CompanyOwner),
                new(User),
                new(Admin)
            };
        }

        public static List<IdentityRole<Guid>> Get()
        {
            return Roles;
        }
    }
}
