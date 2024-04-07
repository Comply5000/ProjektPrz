using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace API.Features.Identity.Commands.SingUp
{
    public class SignUpCommandHandler : IRequestHandler<SingUpCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public SignUpCommandHandler(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Handle(SingUpCommand request, CancellationToken cancellationToken)
                {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }

            //return Unit.Value;
        }
    }
}