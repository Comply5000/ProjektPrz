using API.Common.Exceptions;

namespace API.Features.Companies.Exceptions;

public sealed class CompanyWithNameExistsException : BaseException
{
    public CompanyWithNameExistsException() : base("firma o takiej nazwie już istnieje")
    {
    }
}