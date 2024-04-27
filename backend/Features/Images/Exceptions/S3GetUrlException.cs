using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3GetUrlException : BaseException
    {
        public S3GetUrlException(string errorCode) : base($"Unexpected error while getting URL file from S3Storage: {errorCode}") { }
    }
}
