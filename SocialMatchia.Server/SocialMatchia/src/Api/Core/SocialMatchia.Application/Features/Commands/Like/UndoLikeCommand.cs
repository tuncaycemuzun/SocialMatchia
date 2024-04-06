using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class UndoLikeCommand : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
    }
}
