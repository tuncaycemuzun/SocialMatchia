using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeCommand : IRequest<Result<bool>>
    {
        public required Guid TargetUserId { get;set; }   
    }
}
