using API.Database.Context;
using API.Features.Images.Entities;
using API.Features.Images.Services;
using MediatR;

namespace API.Features.Images.Commands;

public class UploadImageHandler : IRequestHandler<UploadImageCommand, Guid>
{
    private readonly IS3StorageService _s3StorageService;
    private readonly EFContext _context;

    public UploadImageHandler(IS3StorageService s3StorageService, EFContext context)
    {
        _s3StorageService = s3StorageService;
        _context = context;
    }

    public async Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var uploadResult = await _s3StorageService.UploadFileAsync(request.Image, cancellationToken);

        var image = new Image
        {
            Name = request.Image.Name,
            ContentType = request.Image.ContentType,
            TotalBytes = request.Image.Length,
            S3Key = uploadResult,
            Url = string.Empty
        };

        var result = await _context.AddAsync(image, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }
}
