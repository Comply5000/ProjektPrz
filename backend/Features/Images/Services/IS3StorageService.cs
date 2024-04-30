using Microsoft.AspNetCore.Http;

namespace API.Features.Images.Services;

public interface IS3StorageService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    string GetFileUrl(string fileKey);
    Task<MemoryStream> GetFileAsync(string fileKey, CancellationToken cancellationToken);
}


