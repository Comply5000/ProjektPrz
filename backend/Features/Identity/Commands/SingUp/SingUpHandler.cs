using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Static;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace API.Features.Identity.Commands.SingUp
{
    public class SignUpCommandHandler : IRequestHandler<SingUpCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly EFContext _context;

        public SignUpCommandHandler(UserManager<User> userManager, EFContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task Handle(SingUpCommand request, CancellationToken cancellationToken)
        {
            var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email);
            if (isUserExists)
            {
                throw new UserWithEmailExistsExaptions();
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var createdUser = await _userManager.CreateAsync(user, request.Password);

            if (!createdUser.Succeeded)
            {
                throw new CreateUserException(createdUser.Errors);
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);
            if (!addRoleResult.Succeeded)
            {
                throw new AddUserToRoleExeption();
            }


            //return Unit.Value;
        }
    }
}