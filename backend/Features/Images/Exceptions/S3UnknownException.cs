using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3UnknownException : BaseException
    {
         public S3UnknownException() : base("Unknown exception while connecting to S3Service") { }
    }
}
