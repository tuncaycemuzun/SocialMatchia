using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.Like;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class UndoLikeCommand : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
    }

    public class UndoLikeHandler : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.LikeModel.Like> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UndoLikeHandler(IRepository<Domain.Models.LikeModel.Like> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _mediator.Send(new LikeQuery()
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
