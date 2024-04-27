using Amazon.Runtime.Internal;
using MediatR;

namespace API.Features.Images.Commands;

public record UploadImageCommand(IFormFile Image) : IRequest<Guid>;

