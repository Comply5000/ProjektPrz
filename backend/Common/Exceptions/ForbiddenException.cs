namespace API.Common.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException() : base("Akcja jest zabroniona") { }
}