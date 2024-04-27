using API.Features.Images.Services;
using MediatR;

namespace API.Features.Images.Commands;

public class UploadImageHandler : IRequestHandler<UploadImageCommand, Guid>
{
    private readonly IS3StorageService _s3StorageService;

    public UploadImageHandler(IS3StorageService s3StorageService)
    {
        _s3StorageService = s3StorageService;
    }

    public async Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
