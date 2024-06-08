using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3UploadException : BaseException
    {
        public S3UploadException(string errorCode) : base($"Nieoczekiwany błąd podczas przesyłania pliku do S3Storage: {errorCode}") { }
    }
}
