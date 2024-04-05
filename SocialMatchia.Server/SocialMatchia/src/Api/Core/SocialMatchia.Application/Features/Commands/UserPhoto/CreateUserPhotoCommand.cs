using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoCommand : IRequest<Result<bool>>
    {
        public required List<string> Photos { get; set; }
    }
}
