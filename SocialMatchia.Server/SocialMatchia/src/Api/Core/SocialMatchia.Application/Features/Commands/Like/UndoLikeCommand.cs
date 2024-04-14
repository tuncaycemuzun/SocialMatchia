using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Application.Features.Queries.Like;
using SocialMatchia.Common;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class UndoLikeCommand : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
    }

    public class UndoLikeHandler(IRepositoryBase<Domain.Models.Like> repository, CurrentUser currentUser, IMediator mediator) : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.Like> _repository = repository;
        private readonly CurrentUser _currentUser = currentUser;
        private readonly IMediator _mediator = mediator;

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _mediator.Send(new _LikeQuery()
            {
                SourceUserId = _currentUser.Id,
                TargetUserId = request.TargetUserId
            }, cancellationToken);

            if (like is null) return Result.Success(true);

            like.SetIsDeleted(true);
            await _repository.UpdateAsync(like, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
