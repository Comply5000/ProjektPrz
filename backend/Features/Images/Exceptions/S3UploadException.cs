using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3UploadException : BaseException
    {
        public S3UploadException(string errorCode) : base($"Unexpected error while uploading file to S3Storage: {errorCode}") { }
    }
}
