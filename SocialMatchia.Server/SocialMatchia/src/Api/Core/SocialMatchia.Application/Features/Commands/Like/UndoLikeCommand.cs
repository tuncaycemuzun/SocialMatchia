namespace SocialMatchia.Application.Features.Commands
{
    public class UndoLikeCommand : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
    }

    public class UndoLikeHandler : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IRepository<Like> _like;
        private readonly CurrentUser _currentUser;

        public UndoLikeHandler(IRepository<Like> like, CurrentUser currentUser, IMediator mediator)
        {
            _like = like ?? throw new ArgumentNullException(nameof(like));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _like.FirstOrDefaultAsync(new LikeGetSpec(_currentUser.Id, request.TargetUserId), cancellationToken);

            if (like is null) return Result.Success(true);

            like.SetIsDeleted(true);
            await _like.UpdateAsync(like, cancellationToken);
            await _like.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
