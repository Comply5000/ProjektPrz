using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3UnknownException : BaseException
    {
         public S3UnknownException() : base("Nieznany wyjątek podczas łączenia się z usługą S3Service") { }
    }
}
