using API.Common.Exceptions;

namespace API.Features.Companies.Exceptions;

public sealed class CompanyNotFoundException : BaseException
{
    public CompanyNotFoundException() : base("Firm nie istnieje")
    {
    }
}