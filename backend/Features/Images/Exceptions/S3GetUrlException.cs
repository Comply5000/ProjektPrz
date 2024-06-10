using API.Common.Exceptions;

namespace API.Features.Images.Exceptions
{
    public class S3GetUrlException : BaseException
    {
        public S3GetUrlException(string errorCode) : base($"Nieoczekiwany błąd podczas pobierania pliku URL z S3Storage: {errorCode}") { }
    }
}
