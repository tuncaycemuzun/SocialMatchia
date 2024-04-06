using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class DeleteUserPhotoCommand : IRequest<Result<bool>>
    {
        public required Guid Id { get; set; }
    }
}
