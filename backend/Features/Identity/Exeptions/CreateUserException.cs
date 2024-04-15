﻿using API.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Exeptions
{
    public class CreateUserException:BaseException
    {
        public CreateUserException() : base("One or more erros occurred during creating user.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CreateUserException(IEnumerable<IdentityError> errors) : this()
        {
            Errors = errors.GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(a => a.Key, a => a.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
